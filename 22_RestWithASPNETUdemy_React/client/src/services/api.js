import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:57980',
})

export default api;