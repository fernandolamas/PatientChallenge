import React, { useState } from 'react';
import {endpoint} from '../endpoints';

const DeletePatientById = () => {
  const [patientId, setPatientId] = useState('');
  const [patient, setPatient] = useState(null);

  const handlePatientIdChange = (e) => {
    setPatientId(e.target.value);
  };

  const handleFetchPatient = () => {
    // TODO: Add logic to fetch patient by ID
    // For this example, let's assume you have an API endpoint to fetch a patient by ID
    // Replace the API endpoint and add the fetch logic accordingly
    fetch(`${endpoint.Patient}?id=${patientId}`, {
      method:"DELETE",
      headers: {
        'Authorization': `Bearer ${localStorage.getItem("JWTTOKEN")}`,
      }
    })
      .then(response => {
        if(response.status === 404){
          alert("Patient not found")
        }
        if (response.status === 401) {
          alert("Login needed")
        }
        if (response.ok) {
          alert("Patient deleted successfully")
        }

      })
      .catch(error => console.error('Error fetching patient:', error));
  };

  return (
    <div className="container mt-5">
      <h2>Delete Patient by number</h2>
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
        Delete Patient
      </button>
    </div>
  );
};

export default DeletePatientById;
