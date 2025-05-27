import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const Login = () => {
    const [formData, setFormData] = useState({
        email: '',
        password: ''
    });
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await axios.post('/api/auth/login', formData);
            localStorage.setItem('token', response.data.token);
            navigate('/dashboard');
        } catch (err) {
            setError(err.response?.data || 'Ошибка авторизации');
        }
    };

    return (
        <div style={styles.container}>
            <form style={styles.form} onSubmit={handleSubmit}>
                <h2 style={styles.title}>Вход</h2>
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

                <button style={styles.button} type="submit">Войти</button>
            </form>
        </div>
    );
};

const styles = {
    container: {
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        minHeight: '100vh',
        backgroundColor: '#f0f2f5'
    },
    form: {
        backgroundColor: '#fff',
        padding: '2rem',
        borderRadius: '8px',
        boxShadow: '0 2px 4px rgba(0,0,0,0.1)',
        width: '100%',
        maxWidth: '400px'
    },
    title: {
        textAlign: 'center',
        color: '#1877f2',
        marginBottom: '1.5rem',
        fontSize: '24px'
    },
    input: {
        width: '100%',
        padding: '12px',
        marginBottom: '1rem',
        border: '1px solid #dddfe2',
        borderRadius: '6px',
        fontSize: '16px'
    },
    button: {
        width: '100%',
        padding: '12px',
        backgroundColor: '#1877f2',
        color: 'white',
        border: 'none',
        borderRadius: '6px',
        fontSize: '16px',
        fontWeight: 'bold',
        cursor: 'pointer',
        ':hover': {
            backgroundColor: '#166fe5'
        }
    },
    error: {
        color: '#ff0000',
        marginBottom: '1rem',
        textAlign: 'center'
    }
};

export default Login;