import React, { useState } from 'react';
import { endpoint } from './endpoints';

let jwtToken; 

const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const handleUsernameChange = (e) => {
    setUsername(e.target.value);
  };

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
  };

  const handleLogin = (e) => {
    e.preventDefault();

    // TODO: Aquí deberías llamar a tu función para autenticar al usuario
    authenticateUser(username, password);
  };

  return (
    <div className="container mt-5">
      <h2>Login</h2>
      <form onSubmit={handleLogin}>
        <div className="mb-3">
          <label htmlFor="username" className="form-label">Username:</label>
          <input
            type="text"
            className="form-control"
            id="username"
            value={username}
            onChange={handleUsernameChange}
          />
        </div>
        <div className="mb-3">
          <label htmlFor="password" className="form-label">Password:</label>
          <input
            type="password"
            className="form-control"
            id="password"
            value={password}
            onChange={handlePasswordChange}
          />
        </div>
        <button type="submit" className="btn btn-primary">Login</button>
      </form>
    </div>
  );
};

// Simulación de función de autenticación
const authenticateUser = (username, password) => {

  let data = { userName: username, password: password }
  fetch(`${endpoint.Account}`,{
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(data)
  })
  .then(response => {
    if(response.status === 401)
    {
      alert("Invalid credentials")
    }else{
      return response.json()
    }
  })
  .then(data => {
    jwtToken = data.token.token
    localStorage.setItem("JWTTOKEN", jwtToken);
    console.log(data)
    console.log(jwtToken)
    alert('Authenticated OK')
  })
  .catch(error => {
    console.error('Error fetching patient:', error)
    alert("System is unavailable for authentication")
  });
  console.log('Authentication request for user:', { username, password });
};

export default Login;