import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    stages: [
        { duration: '30s', target: 10 },
        { duration: '1m', target: 20 },
        { duration: '30s', target: 0 },
    ],
};

export default function () {
    let res = http.post('http://localhost:5000/api/Auth/Register', JSON.stringify({ username: 'test', password: 'password' }), { headers: { 'Content-Type': 'application/json' } });
    check(res, { 'status was 200': (r) => r.status == 200 });

    res = http.post('http://localhost:5000/api/Auth/Login', JSON.stringify({ username: 'test', password: 'password' }), { headers: { 'Content-Type': 'application/json' } });
    check(res, { 'status was 200': (r) => r.status == 200 });

    res = http.get('http://localhost:5000/api/Auth/IsAuthenticated');
    check(res, { 'status was 200': (r) => r.status == 200 });

    res = http.post('http://localhost:5000/api/Auth/Logout');
    check(res, { 'status was 200': (r) => r.status == 200 });

    sleep(1);
}
