
export function cleanUrl(url) {
    return url.replace(/([^:]\/)\/+/g, "$1");
}
