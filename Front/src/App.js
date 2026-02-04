import React from 'react';
import { BrowserRouter, Routes, Route, Navigate, Link } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import ProtectedRoute from './components/ProtectedRoute';
import Login from './pages/Login';
import Register from './pages/Register';
import Dashboard from './pages/Dashboard';
import Articles from './pages/Articles';
import './App.css';
import './components/Header.css';
import useAuth from './hooks/useAuth';

function App() {
  return (
    
    <BrowserRouter>
      <AuthProvider>
        <div>
          <HeaderNav />
        </div>
        <Routes>
          {/* Public routes */}
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />

          {/* Protected routes */}
          <Route
            path="/dashboard"
            element={
              <ProtectedRoute>
                <Dashboard />
              </ProtectedRoute>
            }
          />

          {/* Default redirect */}
          <Route path="/" element={<Navigate to="/articles" replace />} />
          <Route path="*" element={<Navigate to="/articles" replace />} />
          <Route path="/articles" element={<Articles />} />
        </Routes>
      </AuthProvider>
    </BrowserRouter>
  );
}

function HeaderNav() {
  const { isAuthenticated, logout, user } = useAuth();

  const handleLogout = () => {
    logout();
    window.location.href = '/login';
  };

  return (
    <nav className="navbar">
      <div className="navbar-brand">
        <h1>Britehouse</h1>
        <div className="nav-links">
          <Link to="/">Dashboard</Link>
          <Link to="/articles">Articles</Link>
        </div>
      </div>

      <div className="navbar-menu">
        {isAuthenticated ? (
          <>
            <span className="user-name">Welcome, {user?.email || user?.userId || 'user'}!</span>
            <button className="logout-btn" onClick={handleLogout}>
              Logout
            </button>
          </>
        ) : (
          <>
            <Link to="/login" className="login-link">Login</Link>
            <Link to="/register" className="register-link">Register</Link>
          </>
        )}
      </div>
    </nav>
  );
}

export default App;
