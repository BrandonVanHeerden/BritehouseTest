import React from 'react';
import { useAuth } from '../hooks/useAuth';
import './Dashboard.css';

export const Dashboard = () => {
  const { user, logout } = useAuth();

  const handleLogout = () => {
    logout();
    window.location.href = '/login';
  };

  return (
    <div className="dashboard-container">
      {/* Global header is used app-wide; remove duplicate navbar here */}

      <div className="dashboard-content">
        <div className="container">
          <h2>Dashboard</h2>
          <p>Welcome to your Britehouse dashboard!</p>

          <div className="user-info-card">
            <h3>User Information</h3>
            <p>
              <strong>Name:</strong> {user?.firstName} {user?.lastName}
            </p>
            <p>
              <strong>Email:</strong> {user?.email}
            </p>
            {user?.role && (
              <p>
                <strong>Role:</strong> {user.role}
              </p>
            )}
          </div>

          <div className="getting-started">
            <h3>Getting Started</h3>
            <ul>
              <li>Customize the dashboard with your own content</li>
              <li>Add more routes in the App.js file</li>
              <li>Create additional components in the src/components directory</li>
              <li>Update the API service in src/services/ to match your backend</li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
