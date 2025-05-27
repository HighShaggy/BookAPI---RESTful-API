import Login from './Login';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';


const Register = () => {
    const [formData, setFormData] = useState({
        email: '',
        password: ''
    });
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await axios.post('/api/auth/register', formData);
            localStorage.setItem('token', response.data.token);
            navigate('/dashboard');
        } catch (err) {
            setError(err.response?.data || 'Ошибка регистрации');
        }
    };

    return (
        <div style={styles.container}>
            <form style={styles.form} onSubmit={handleSubmit}>
                <h2 style={styles.title}>Регистрация</h2>
                {error && <div style={styles.error}>{error}</div>}

                <input
                    style={styles.input}
                    type="email"
                    placeholder="Email"
                    value={formData.email}
                    onChange={e => setFormData({...formData, email: e.target.value})}
                    required
                />

                <input
                    style={styles.input}
                    type="password"
                    placeholder="Пароль"
                    value={formData.password}
                    onChange={e => setFormData({...formData, password: e.target.value})}
                    required
                />

                <button style={styles.button} type="submit">Зарегистрироваться</button>
            </form>
        </div>
    );
};

// Используем те же стили, что и для Login
const styles = {
    ...Login.styles,
    title: {
        ...Login.styles.title,
        color: '#42b72a'
    },
    button: {
        ...Login.styles.button,
        backgroundColor: '#42b72a',
        ':hover': {
            backgroundColor: '#36a420'
        }
    }
};

export default Register;