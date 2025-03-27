// Define an array of URLs to smoke test
const urls = [
    { url: "http://localhost:5260/cars", expectedStatusCode: 200 },
    { url: "http://localhost:5260/registrations", expectedStatusCode: 200 },
    // { url: "http://localhost:5260/guides/tours", expectedStatusCode: 200 },
    // Add more URLs as needed
  ];
  

  import http from 'k6/http';
  import { check } from 'k6';
  
  export default function () {
    urls.forEach(({ url, expectedStatusCode }) => {
      // Send a GET request to the URL
      let response = http.get(url);
  
      // Verify the response status code
      check(response, {
        [`Status is ${expectedStatusCode} for ${url}`]: (r) => r.status === expectedStatusCode,
      });
    });
  }
  