import axiosInstance from './axiosInstance';

// Authentication endpoints
export const authService = {
  // Login with email and password (SignInQuery via GET with query params)
  login: (email, password) => {
    return axiosInstance.get('/Authentication/sign-in', {
      params: {
        Email: email,
        Password: password,
      },
    });
  },

  // Register a new user (SignUpCommand)
  register: (Name, Surname, Email, Cell, Id, Password, Role) => {
    return axiosInstance.post('/Account/sign-up', {
      Name,
      Surname,
      Email,
      Cell,
      Id,
      Password,
      Role,
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
