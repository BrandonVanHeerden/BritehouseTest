import React, { createContext, useState, useCallback, useEffect } from 'react';
import { jwtDecode } from 'jwt-decode';
import authService from '../services/authService';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // Initialize auth state from localStorage
  useEffect(() => {
    const initializeAuth = () => {
      try {
        const token = localStorage.getItem('authToken');
        const savedUser = localStorage.getItem('user');

        if (token && savedUser) {
          // Check if token is still valid
          const decoded = jwtDecode(token);
          const currentTime = Date.now() / 1000;

          if (decoded.exp > currentTime) {
            setUser(JSON.parse(savedUser));
          } else {
            // Token expired
            localStorage.removeItem('authToken');
            localStorage.removeItem('user');
          }
        }
      } catch (err) {
        console.error('Error initializing auth:', err);
        localStorage.removeItem('authToken');
        localStorage.removeItem('user');
      } finally {
        setLoading(false);
      }
    };

    initializeAuth();
  }, []);

  // Login function
  const login = useCallback(async (email, password) => {
    try {
      setError(null);
      const response = await authService.login(email, password);
      const { token, user: userData } = response.data;

      // Store token and user info
      localStorage.setItem('authToken', token);
      localStorage.setItem('user', JSON.stringify(userData));

      setUser(userData);
      return response.data;
    } catch (err) {
      const errorMessage =
        err.response?.data?.message || 'Login failed. Please try again.';
      setError(errorMessage);
      throw err;
    }
  }, []);

  // Register function
  const register = useCallback(async (email, password, firstName, lastName) => {
    try {
      setError(null);
      const response = await authService.register(email, password, firstName, lastName);
      const { token, user: userData } = response.data;

      // Store token and user info
      localStorage.setItem('authToken', token);
      localStorage.setItem('user', JSON.stringify(userData));

      setUser(userData);
      return response.data;
    } catch (err) {
      const errorMessage =
        err.response?.data?.message || 'Registration failed. Please try again.';
      setError(errorMessage);
      throw err;
    }
  }, []);

  // Logout function
  const logout = useCallback(() => {
    authService.logout();
    setUser(null);
    setError(null);
  }, []);

  // Check if user is authenticated
  const isAuthenticated = !!user;

  const value = {
    user,
    loading,
    error,
    isAuthenticated,
    login,
    register,
    logout,
  };

  return (
    <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
  );
};

export default AuthContext;
