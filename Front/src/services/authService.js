import axiosInstance from './axiosInstance';

// Authentication endpoints
export const authService = {
  // Login with email and password (API expects SignInQuery with Email, Password)
  login: (email, password) => {
    return axiosInstance.post('/api/v1/Authentication/sign-in', {
      Email: email,
      Password: password,
    });
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
