import { displayResponse, get } from "/js/ApiCalls.js";

async function getNoAuth() {
    const response = await get(`Claims/UserInfo`);
    displayResponse(response);
}
async function getAuth() {
    const response = await get(`Claims/TestAuth`);
    displayResponse(response);
}

async function getPilot() {
    const response = await get(`Claims/IsInRole/Pilot`);
    displayResponse(response);
}

async function getEngineer() {
    const response = await get(`Claims/IsInRole/Engineer`);
    displayResponse(response);
}

document.querySelector('#get-no-auth').addEventListener('click', getNoAuth);
document.querySelector('#get-auth').addEventListener('click', getAuth);
document.querySelector('#get-pilot').addEventListener('click', getPilot);
document.querySelector('#get-engineer').addEventListener('click', getEngineer);