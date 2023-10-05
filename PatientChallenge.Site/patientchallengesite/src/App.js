import React from 'react';
import { Link, Routes, Route, Outlet } from 'react-router-dom';
import Login from './Authenticate'
import AddPatient from './Patient/AddPatient'
import GetPatient from './Patient/GetPatient'
import UpdatePatient from './Patient/UpdatePatient'
import DeletePatient from './Patient/DeletePatient'
import 'bootstrap/dist/css/bootstrap.min.css';



export default function App() {
  return (
    <div>
    <div className="container d-flex flex-column justify-content-center align-items-center vh-100">
      <h1 className="text-center mb-4">Patient Challenge</h1>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route path="Authenticate" element={<Login />} />
          <Route path="AddPatient" element={<AddPatient />} />
          <Route path="GetPatient" element={<GetPatient />} />
          <Route path="UpdatePatient" element={<UpdatePatient />} />
          <Route path="DeletePatient" element={<DeletePatient />} />
        </Route>
      </Routes>
    </div>
  </div>
);
}

function Layout() {
  return (
    <div className="container mt-5">
      <div className="row">
        <div className="col">
          <nav className="mb-4">
            <ul className="nav nav-pills justify-content-center">
            <li className="nav-item">
                <Link className="nav-link" to="/Authenticate">
                  <button className="btn btn-primary mb-2">Login</button>
                </Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/AddPatient">
                  <button className="btn btn-primary mb-2">Create a new patient</button>
                </Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/GetPatient">
                <button className="btn btn-primary mb-2">Get a specific patient</button>
                  
                </Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/UpdatePatient">
                  <button className="btn btn-primary mb-2">Update patient</button>
                </Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/DeletePatient">
                  <button className="btn btn-primary mb-2">Delete patient</button>
                </Link>
              </li>
            </ul>
          </nav>
        </div>
      </div>

      <hr />

      <div className="row">
        <div className="col">
          <Outlet />
        </div>
      </div>
    </div>
  );
}