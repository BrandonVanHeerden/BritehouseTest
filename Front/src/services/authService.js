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
  register: (Name, Surname, Email, Cell, Id, Password, Role, Roles) => {
    // Roles is expected to be an array of role GUIDs (optional). Keep Role for backward compatibility.
    const payload = {
      Name,
      Surname,
      Email,
      Cell,
      Id,
      Password,
    };
    if (Role) payload.Role = Role;
    if (Roles && Array.isArray(Roles)) payload.Roles = Roles;

    return axiosInstance.post('/Account/sign-up', payload);
  },

  // Refresh token
  refreshToken: () => {
    return axiosInstance.post('/auth/refresh');
  },

  // Logout
  logout: () => {
    try {
      // Remove tokens and user info from storage
      localStorage.removeItem('authToken');
      localStorage.removeItem('refreshToken');
      localStorage.removeItem('user');

      // Also clear sessionStorage variants if used elsewhere
      try {
        sessionStorage.removeItem('authToken');
        sessionStorage.removeItem('refreshToken');
        sessionStorage.removeItem('user');
      } catch (e) {
        // ignore if sessionStorage not available
      }

      // Clear axios default Authorization header if present
      try {
        if (axiosInstance && axiosInstance.defaults && axiosInstance.defaults.headers) {
          delete axiosInstance.defaults.headers.common.Authorization;
        }
      } catch (e) {
        // ignore
      }

      // Clear browser caches (best-effort)
      if (typeof window !== 'undefined' && window.caches) {
        caches.keys().then((names) => names.forEach((n) => caches.delete(n)));
      }
    } catch (err) {
      // best-effort cleanup
      console.warn('Error during logout cleanup', err);
    }
  },

  // Get current user profile
  getCurrentUser: () => {
    return axiosInstance.get('/auth/me');
  },
};

export default authService;
