import http from 'k6/http';
import { sleep } from 'k6';
import * as config from './config.js';

export const options = {
    vus: 100,
    duration: '10s',

    thresholds: {
        http_req_duration: ['p(95)<1000']
    },
};

export default function () {
    http.get(config.API_REVERSE_URL_TRAVELERS);
    sleep(1);
}