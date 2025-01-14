export async function getCustomAuthData(headerName, headerVal) {
    var headers = {};
    headers[headerName] = headerVal;

    const request1 = new Request("/CustomAuthScheme/GetData", {
        method: "GET",
        headers: headers
    });

    const response = await fetch(request1);
    return response;
}

export async function getCraftDetailApi(craft) {
    const request = new Request(`ResourceAuthPolicy/Craft/${craft}`, {
        method: "GET"
    });

    const response = await fetch(request);
    return response;
}

export async function getCraftAtLaunchsite(launchSite) {
    const request = new Request(`AuthPolicy/LaunchSite/${launchSite}`, {
        method: "GET"
    });

    const response = await fetch(request);
    return response;
}