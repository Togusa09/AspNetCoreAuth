import { displayResponse, get } from "/js/ApiCalls.js";

async function getNoAuth() {
    const response = await get(`Task02/Anonymous`);
    await displayResponse(response);
}
async function getAuth() {
    const response = await get(`Task02/Authenticated`);
    await displayResponse(response);
}

document.querySelector('#get-no-auth').addEventListener('click', getNoAuth);
document.querySelector('#get-auth').addEventListener('click', getAuth);