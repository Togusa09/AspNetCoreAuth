import { getCraftAtLaunchsite, displayResponse } from "/js/ApiCalls.js";

async function getCraft() {
    const launchSite = document.getElementById("launch-site").value;
    const response = await getCraftAtLaunchsite(launchSite);

    displayResponse(response);
}

async function getLocation() {
    const launchSite = document.getElementById("launch-site").value;
    const response = await getCraftAtLaunchsite(launchSite);

    displayResponse(response);
}

//async function getMissionControl() {
//    const response = await get(`AuthPolicy/CapeCanaveral/MissionControl`);
//    displayResponse(response);
//}

//async function getLaunchPad() {
//    const response = await get(`AuthPolicy/CapeCanaveral/LaunchPad`);
//    displayResponse(response);
//}

document.querySelector('#get-location').addEventListener('click', getLocation);