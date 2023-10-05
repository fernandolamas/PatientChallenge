import React, { useState } from 'react';
import { endpoint } from '../endpoints';

const GetPatientById = () => {
  const [patientId, setPatientId] = useState('');
  const [patient, setPatient] = useState(null);

  const handlePatientIdChange = (e) => {
    setPatientId(e.target.value);
  };

  const handleFetchPatient = () => {
    fetch(`${endpoint.Patient}?id=${patientId}`,
      {
        method: 'GET',
        headers: {
          'Authorization': `Bearer ${localStorage.getItem("JWTTOKEN")}`
        }
      }
    )
      .then(response => {
        if (response.status === 404) {
          alert("Not found");
        }
        if (response.status === 401) {
          alert("Login needed")
        }
        if(response.status === 200)
        {
          return response.json()
        }
      })
      .then(data => setPatient(data))
      .catch(error => {
        console.error('Error fetching patient:', error)
      });
  };

  return (
    <div className="container mt-5">
      <h2>Get Patient by number</h2>
      <div className="mb-3">
        <label htmlFor="patientId" className="form-label">Patient number:</label>
        <input
          type="text"
          className="form-control"
          id="patientId"
          value={patientId}
          onChange={handlePatientIdChange}
        />
      </div>
      <button
        type="button"
        className="btn btn-primary"
        onClick={handleFetchPatient}
      >
        Fetch Patient
      </button>
      {patient && (
        <div className="mt-4">
          <h3>Patient Information</h3>
          <p><strong>Name:</strong> {patient.name}</p>
          <p><strong>Username:</strong> {patient.userName}</p>
          <p><strong>Password:</strong> {patient.password}</p>
          <p><strong>Is Active:</strong> {patient.isActive ? 'Yes' : 'No'}</p>
          <p><strong>Role:</strong> {patient.role}</p>
        </div>
      )}
    </div>
  );
};

export default GetPatientById;
