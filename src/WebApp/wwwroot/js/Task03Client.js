import { displayResponse, get } from "/js/ApiCalls.js";

async function getClaims() {
    const response = await get(`Task03/GetClaims`);
    await displayResponse(response);
}

document.querySelector('#get-claims').addEventListener('click', getClaims);