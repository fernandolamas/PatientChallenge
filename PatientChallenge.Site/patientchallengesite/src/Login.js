// Login.js
import React, { useState } from 'react';
import createProxy from './useProxy';

const Login = () => {
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');

    const handleUserNameChange = (e) => {
        setUserName(e.target.value);
    };

    const handlePasswordChange = (e) => {
        setPassword(e.target.value);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const data = {
            userName,
            password
        };

        try {
            const response = await fetch('https://localhost:7105/Account', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });
            response.json().then(jwt => {
                console.log(jwt);
                debugger
                createProxy();

            }).catch(err => {
                console.error(err);
            });
            debugger
            if (response.ok) {
                console.log('Usuario autenticado con éxito');
            } else {
                console.error('Error al autenticar usuario');
            }
        } catch (error) {
            console.error('Error de red:', error);
        }
    };

    return (
        <div>
            <h2>Login</h2>
            <form onSubmit={handleSubmit}>
                <label>
                    UserName:
                    <input type="userName" value={userName} onChange={handleUserNameChange} />
                </label>
                <br />
                <label>
                    Password:
                    <input type="password" value={password} onChange={handlePasswordChange} />
                </label>
                <br />
                <button type="submit">Login</button>
            </form>
        </div>
    );
};


export default Login;
