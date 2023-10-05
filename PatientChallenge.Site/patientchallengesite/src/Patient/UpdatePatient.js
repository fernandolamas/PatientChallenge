import React, { useState } from 'react';
import { endpoint } from '../endpoints';

const UpdatePatient = () => {
  const [id, setId] = useState('');
  const [name, setName] = useState('');
  const [userName, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [isActive, setIsActive] = useState(false);
  let [role, setRole] = useState('User');

  const [nameTouched, setNameTouched] = useState(false);
  const [userNameTouched, setUserNameTouched] = useState(false);
  const [passwordTouched, setPasswordTouched] = useState(false);
  const [roleTouched, setRoleTouched] = useState(false);

  let [patient, setPatient] = useState(null);

  const handleIdChange = (e) => {
    setId(e.target.value);
  };

  const handleNameChange = (e) => {
    setName(e.target.value);
    setNameTouched(true);
  };

  const handleUsernameChange = (e) => {
    setUsername(e.target.value);
    setUserNameTouched(true);
  };

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
    setPasswordTouched(true);
  };

  const handleIsActiveChange = (e) => {
    setIsActive(e.target.checked);
  };

  const handleRoleChange = (e) => {
    setRole(e.target.value);
    setRoleTouched(true);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    
    if (role == "User") {
      role = 1;
    } else {
      role = 0;
    }

    let data = {
      id: id,
      name: name,
      userName: userName,
      password: password,
      isActive: isActive,
      role: role
    }
    fetch(`${endpoint.Patient}`, {
      method: 'PUT',
      headers: {
        'Authorization': `Bearer ${localStorage.getItem("JWTTOKEN")}`,
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    })
      .then(response => {
        if (!response.ok) {
          switch (response.status) {
            case 400:
              alert("Data provided is invalid");  
              break;
            case 401:
              alert("Login needed")
              break;
            case 409:
              alert("Username already exists")
              break;
            default:
              alert(`System is unavailable for patient creation`)
              break;
          }
        }else{
          // Reset form fields after submission
          setName('');
          setUsername('');
          setPassword('');
          setIsActive(false);
          setRole('User');
          setNameTouched(false);
          setUserNameTouched(false);
          setPasswordTouched(false);
          setRoleTouched(false);
          return response.json();
        }
      })
      .then(data => {
        console.log('Server response', data);
        setPatient(data);
      })
      .catch(error => {
        console.error('Error sending request', error);
      });


  };


  return (
    <div className="container mt-5">
      <h2>Update an existing patient</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="id" className="form-label">Patient number to be updated</label>
          <input
            type="text"
            className="form-control"
            id="id"
            value={id}
            onChange={handleIdChange}
          />
        </div>
        <div className="mb-3">
          <p>Complete the blank fields with new information</p>
        </div>
        <div className="mb-3">
          <label htmlFor="name" className="form-label">Name:</label>
          <input
            type="text"
            className={`form-control ${nameTouched && (name.length < 5 || name.length > 100) ? 'is-invalid' : ''}`}
            id="name"
            value={name}
            onChange={handleNameChange}
          />
          {nameTouched && (name.length < 5 || name.length > 100) ? (
            <div className="invalid-feedback">Must be between 5 and 100 characters</div>
          ) : null}
        </div>

        <div className="mb-3">
          <label htmlFor="userName" className="form-label">Username:</label>
          <input
            type="text"
            className={`form-control ${userNameTouched && (userName.length < 5 || userName.length > 50) ? 'is-invalid' : ''}`}
            id="userName"
            value={userName}
            onChange={handleUsernameChange}
          />
          {userNameTouched && (userName.length < 5 || userName.length > 50) ? (
            <div className="invalid-feedback">Must be between 5 and 50 characters</div>
          ) : null}
        </div>

        <div className="mb-3">
          <label htmlFor="password" className="form-label">Password:</label>
          <input
            type="password"
            className={`form-control ${passwordTouched && (password.length < 5 || password.length > 50) ? 'is-invalid' : ''}`}
            id="password"
            value={password}
            onChange={handlePasswordChange}
          />
          {passwordTouched && (password.length < 5 || password.length > 50) ? (
            <div className="invalid-feedback">Must be between 5 and 50 characters</div>
          ) : null}
        </div>

        <div className="mb-3 form-check">
          <input
            type="checkbox"
            className="form-check-input"
            id="isActive"
            checked={isActive}
            onChange={handleIsActiveChange}
          />
          <label className="form-check-label" htmlFor="isActive">Is Active</label>
        </div>

        <div className="mb-3">
          <label htmlFor="role" className="form-label">Role:</label>
          <select
            className={`form-select ${roleTouched && (role !== 'User' && role !== 'Administrator') ? 'is-invalid' : ''}`}
            id="role"
            value={role}
            onChange={handleRoleChange}
          >
            <option value="User">User</option>
            <option value="Administrator">Administrator</option>
          </select>
          {roleTouched && (role !== 'User' && role !== 'Administrator') ? (
            <div className="invalid-feedback">Invalid role selected</div>
          ) : null}
        </div>
        <button type="submit" className="btn btn-primary">Create Patient</button>
        
      </form>
      {patient && (
        <div className="mt-4">
          <h3>Patient Information</h3>
          <p><strong>Name:</strong> {patient.name}</p>
          <p><strong>Username:</strong> {patient.userName}</p>
          <p><strong>Password:</strong> {patient.password}</p>
          <p><strong>Is Active:</strong> {patient.isActive ? 'Yes' : 'No'}</p>
          <p><strong>Role:</strong> {patient.role}</p>
        </div>)}
    </div>

  );
};

export default UpdatePatient;
