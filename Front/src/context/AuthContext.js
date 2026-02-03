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
      const data = response.data;

      if (!data?.isSuccess) {
        const msg = data?.error?.message || 'Login failed';
        setError(msg);
        throw new Error(msg);
      }

      const value = data.value || {};
      const accessToken = value.accessToken || value.token || null;
      const refresh = value.refreshToken || null;

      const userData = {
        userId: value.userId,
        email: value.email,
      };

      // Store token and user info
      if (accessToken) localStorage.setItem('authToken', accessToken);
      if (refresh) localStorage.setItem('refreshToken', refresh);
      localStorage.setItem('user', JSON.stringify(userData));

      setUser(userData);
      return data;
    } catch (err) {
      const errorMessage =
        err.response?.data?.message || 'Login failed. Please try again.';
      setError(errorMessage);
      throw err;
    }
  }, []);

  // Register function
  const register = useCallback(
    async (email, password, firstName, lastName) => {
      try {
        setError(null);

        // Map available fields to SignUpCommand expected by API
        const Name = firstName || '';
        const Surname = lastName || '';
        const Email = email || '';
        const Cell = '';
        const Id = '';
        const Password = password || '';
        const Role = 'User';

        const response = await authService.register(
          Name,
          Surname,
          Email,
          Cell,
          Id,
          Password,
          Role
        );

        const data = response.data;
        if (!data?.isSuccess) {
          const msg = data?.error?.message || 'Registration failed';
          setError(msg);
          throw new Error(msg);
        }

        return data.value;
      } catch (err) {
        const errorMessage =
          err.response?.data?.message || 'Registration failed. Please try again.';
        setError(errorMessage);
        throw err;
      }
    },
    []
  );

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
