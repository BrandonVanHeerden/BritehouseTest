import React from 'react';
import { BrowserRouter, Routes, Route, Navigate, Link } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import ProtectedRoute from './components/ProtectedRoute';
import Login from './pages/Login';
import Register from './pages/Register';
import Dashboard from './pages/Dashboard';
import Articles from './pages/Articles';
import './App.css';
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
  const { isAuthenticated, logout } = useAuth();

  return (
    <header className="app-header">
      <nav>
        <Link to="/">Dashboard</Link>
        <Link to="/articles">Articles</Link>
        {isAuthenticated ? (
          <button onClick={() => { logout(); window.location.href = '/login'; }} style={{ marginLeft: 8 }}>
            Logout
          </button>
        ) : (
          <>
            <Link to="/login">Login</Link>
            <Link to="/register">Register</Link>
          </>
        )}
      </nav>
    </header>
  );
}

export default App;
