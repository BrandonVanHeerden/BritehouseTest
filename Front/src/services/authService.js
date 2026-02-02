import axiosInstance from './axiosInstance';

// Authentication endpoints
export const authService = {
  // Login with email and password
  login: (email, password) => {
    return axiosInstance.post('/auth/login', { email, password });
  },

  // Register a new user
  register: (email, password, firstName, lastName) => {
    return axiosInstance.post('/auth/register', {
      email,
      password,
      firstName,
      lastName,
    });
  },

  // Refresh token
  refreshToken: () => {
    return axiosInstance.post('/auth/refresh');
  },

  // Logout
  logout: () => {
    localStorage.removeItem('authToken');
    localStorage.removeItem('user');
  },

  // Get current user profile
  getCurrentUser: () => {
    return axiosInstance.get('/auth/me');
  },
};

export default authService;
