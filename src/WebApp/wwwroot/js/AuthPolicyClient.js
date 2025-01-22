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
    const launchSite = document.getElementById("location").value;
    //const response = await getCraft(launchSite);

    var response = await get(`AuthPolicy/${launchSite}`);

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