import http from 'k6/http';
import { check, sleep } from 'k6';
import { Counter } from 'k6/metrics';

export let errorCount = new Counter('errors');

export let options = {
    stages: [
        { duration: '30s', target: 20 }, // Ramp-up to 20 users
        { duration: '1m', target: 20 },  // Stay at 20 users for 1 minute
        { duration: '10s', target: 0 },  // Ramp-down to 0 users
    ],
};

const BASE_URL = 'http://localhost:5000/api';

export default function () {
    let responses = http.batch([
        ['POST', `${BASE_URL}/auth/Register`, JSON.stringify({ username: 'test', password: 'test' }), { headers: { 'Content-Type': 'application/json' } }],
        ['POST', `${BASE_URL}/auth/Login`, JSON.stringify({ username: 'test', password: 'test' }), { headers: { 'Content-Type': 'application/json' } }],
        ['GET', `${BASE_URL}/carManagement`],
        ['GET', `${BASE_URL}/carManagement/1`],
        ['POST', `${BASE_URL}/carManagement`, JSON.stringify({ make: 'Test', model: 'Test', year: 2020 }), { headers: { 'Content-Type': 'application/json' } }],
    ]);

    check(responses[0], {
        'register status was 200': (r) => r.status === 200,
    }) || errorCount.add(1);

    check(responses[1], {
        'login status was 200': (r) => r.status === 200,
    }) || errorCount.add(1);

    check(responses[2], {
        'get all cars status was 200': (r) => r.status === 200,
    }) || errorCount.add(1);

    check(responses[3], {
        'get car status was 200': (r) => r.status === 200,
    }) || errorCount.add(1);

    check(responses[4], {
        'post car status was 201': (r) => r.status === 201,
    }) || errorCount.add(1);

    sleep(1);
}
