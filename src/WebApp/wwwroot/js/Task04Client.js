import { get, displayResponse } from "/js/ApiCalls.js";

//async function getCraft() {
//    const launchSite = document.getElementById("launch-site").value;
//    const response = await getCraftAtLaunchsite(launchSite);

//    displayResponse(response);
//}

//export function getCraftDetailApi(craft) {
//    return get(`ResourceAuthPolicy/Craft/${craft}`);
//}


async function getLocation() {
    const location = document.getElementById("location").value;

    var response = await get(`Task04/${location}`);

    await displayResponse(response);
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