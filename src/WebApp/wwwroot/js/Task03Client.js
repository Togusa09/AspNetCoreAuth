import { displayResponse, get } from "/js/ApiCalls.js";

async function getNoAuth() {
    const response = await get(`Task03/UserInfo`);
    await displayResponse(response);
}
async function getAuth() {
    const response = await get(`Task03/TestAuth`);
    await displayResponse(response);
}

async function getPilot() {
    const response = await get(`Task03/IsInRole/Pilot`);
    await displayResponse(response);
}

async function getFlightDirector() {
    const response = await get(`Task03/IsInRole/FlightDirector`);
    await displayResponse(response);
}

document.querySelector('#get-no-auth').addEventListener('click', getNoAuth);
document.querySelector('#get-auth').addEventListener('click', getAuth);
document.querySelector('#get-pilot').addEventListener('click', getPilot);
document.querySelector('#get-flight-director').addEventListener('click', getFlightDirector);