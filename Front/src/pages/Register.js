import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../hooks/useAuth';
import './Register.css';

export const Register = () => {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    confirmPassword: '',
  });
  const [selectedRoles, setSelectedRoles] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [validationError, setValidationError] = useState('');
  const navigate = useNavigate();
  const { register, error } = useAuth();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
    setValidationError('');
  };

  const toggleRole = (roleId) => {
    setSelectedRoles((prev) => {
      if (prev.includes(roleId)) return prev.filter((r) => r !== roleId);
      return [...prev, roleId];
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setValidationError('');

    // Validate form
    if (formData.password !== formData.confirmPassword) {
      setValidationError('Passwords do not match');
      return;
    }

    if (formData.password.length < 6) {
      setValidationError('Password must be at least 6 characters');
      return;
    }

    setIsLoading(true);

    try {
      await register(
        formData.email,
        formData.password,
        formData.firstName,
        formData.lastName,
        selectedRoles
      );
      // After successful signup, redirect to login so user can sign in
      navigate('/login');
    } catch (err) {
      console.error('Registration error:', err);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="register-container">
      <div className="register-card">
        <h1>Britehouse</h1>
        <h2>Create Account</h2>

        {(error || validationError) && (
          <div className="error-message">
            {error || validationError}
          </div>
        )}

        <form onSubmit={handleSubmit}>
          <div className="form-row">
            <div className="form-group">
              <label htmlFor="firstName">First Name</label>
              <input
                id="firstName"
                type="text"
                name="firstName"
                value={formData.firstName}
                onChange={handleChange}
                required
                placeholder="First name"
                disabled={isLoading}
              />
            </div>

            <div className="form-group">
              <label htmlFor="lastName">Last Name</label>
              <input
                id="lastName"
                type="text"
                name="lastName"
                value={formData.lastName}
                onChange={handleChange}
                required
                placeholder="Last name"
                disabled={isLoading}
              />
            </div>
          </div>

          <div className="form-group">
            <label htmlFor="email">Email</label>
            <input
              id="email"
              type="email"
              name="email"
              value={formData.email}
              onChange={handleChange}
              required
              placeholder="Enter your email"
              disabled={isLoading}
            />
          </div>

          <div className="form-group">
            <label htmlFor="password">Password</label>
            <input
              id="password"
              type="password"
              name="password"
              value={formData.password}
              onChange={handleChange}
              required
              placeholder="Enter password (min 6 characters)"
              disabled={isLoading}
            />
          </div>

          <div className="form-group">
            <label htmlFor="confirmPassword">Confirm Password</label>
            <input
              id="confirmPassword"
              type="password"
              name="confirmPassword"
              value={formData.confirmPassword}
              onChange={handleChange}
              required
              placeholder="Confirm password"
              disabled={isLoading}
            />
          </div>

          <div className="form-group roles-group">
            <label>Roles</label>
            <div className="roles-options">
              <label>
                <input
                  type="checkbox"
                  value="55676D85-48D3-4995-B827-790FC0634600"
                  checked={selectedRoles.includes('55676D85-48D3-4995-B827-790FC0634600')}
                  onChange={() => toggleRole('55676D85-48D3-4995-B827-790FC0634600')}
                  disabled={isLoading}
                />
                Admin
              </label>

              <label>
                <input
                  type="checkbox"
                  value="CBC3B84E-BEAC-40B7-B31D-0C2FBC527148"
                  checked={selectedRoles.includes('CBC3B84E-BEAC-40B7-B31D-0C2FBC527148')}
                  onChange={() => toggleRole('CBC3B84E-BEAC-40B7-B31D-0C2FBC527148')}
                  disabled={isLoading}
                />
                Author
              </label>

              <label>
                <input
                  type="checkbox"
                  value="5F78F802-1FA2-4AF6-BC81-269AA1B5181A"
                  checked={selectedRoles.includes('5F78F802-1FA2-4AF6-BC81-269AA1B5181A')}
                  onChange={() => toggleRole('5F78F802-1FA2-4AF6-BC81-269AA1B5181A')}
                  disabled={isLoading}
                />
                Reader
              </label>
            </div>
            <small className="roles-help">Select one or more roles to assign to this account.</small>
          </div>

          <button type="submit" disabled={isLoading}>
            {isLoading ? 'Creating Account...' : 'Register'}
          </button>
        </form>

        <p className="login-link">
          Already have an account?{' '}
          <a href="/login">Login here</a>
        </p>
      </div>
    </div>
  );
};

export default Register;
