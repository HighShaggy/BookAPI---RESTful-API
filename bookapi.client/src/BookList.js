import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { css } from '@emotion/react';

function BookList() {
    const [books, setBooks] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchBooks = async () => {
            try {
                const response = await axios.get('/api/book');
                setBooks(response.data);
                setLoading(false);
            } catch (error) {
                console.error('Ошибка при загрузке книг:', error);
                setLoading(false);
            }
        };

        fetchBooks();
    }, []);

    if (loading) return (
        <div style={styles.loadingContainer}>
            <div style={styles.spinner}></div>
            <p style={styles.loadingText}>Загрузка книг...</p>
        </div>
    );

    return (
        <div style={styles.container}>
            <h1 style={styles.header}>Библиотека книг</h1>

            <div style={styles.booksGrid}>
                {books.map(book => (
                    <div key={book.id} style={styles.bookCard}>
                        <div style={styles.cardContent}>
                            <h3 style={styles.bookTitle}>{book.title}</h3>
                            <p style={styles.bookAuthor}>{book.author}</p>
                            {book.year && <p style={styles.bookYear}>Год издания: {book.year}</p>}
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}

const styles = {
    container: {
        maxWidth: '1200px',
        margin: '0 auto',
        padding: '2rem',
        fontFamily: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif"
    },
    header: {
        textAlign: 'center',
        color: '#2c3e50',
        fontSize: '2.5rem',
        marginBottom: '2rem',
        textTransform: 'uppercase',
        letterSpacing: '2px'
    },
    booksGrid: {
        display: 'grid',
        gridTemplateColumns: 'repeat(auto-fill, minmax(250px, 1fr))',
        gap: '1.5rem',
        padding: '1rem'
    },
    bookCard: {
        backgroundColor: '#ffffff',
        borderRadius: '10px',
        boxShadow: '0 4px 6px rgba(0, 0, 0, 0.1)',
        transition: 'transform 0.3s ease',
        overflow: 'hidden',
        ':hover': {
            transform: 'translateY(-5px)'
        }
    },
    cardContent: {
        padding: '1.5rem'
    },
    bookTitle: {
        color: '#34495e',
        fontSize: '1.2rem',
        marginBottom: '0.5rem',
        fontWeight: '600'
    },
    bookAuthor: {
        color: '#7f8c8d',
        fontSize: '1rem',
        fontStyle: 'italic',
        marginBottom: '0.5rem'
    },
    bookYear: {
        color: '#95a5a6',
        fontSize: '0.9rem'
    },
    loadingContainer: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center',
        height: '100vh'
    },
    spinner: {
        border: '4px solid #f3f3f3',
        borderTop: '4px solid #3498db',
        borderRadius: '50%',
        width: '40px',
        height: '40px',
        animation: 'spin 1s linear infinite',
        marginBottom: '1rem'
    },
    loadingText: {
        color: '#3498db',
        fontSize: '1.2rem'
    },
    '@keyframes spin': {
        '0%': { transform: 'rotate(0deg)' },
        '100%': { transform: 'rotate(360deg)' }
    }
};

export default BookList;