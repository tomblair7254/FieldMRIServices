let warningTimeout;
let logoutTimeout;

function showToast(message) {
	const toast = document.createElement("div");
	toast.innerHTML = `<ejs-toast id="toast" title="Notification" content="${message}" position='TopRight'></ejs-toast>`;
	document.body.appendChild(toast);
	const toastObj = new ej.notifications.Toast({ timeOut: 5000 });
	toastObj.appendTo("#toast");
}

function startSessionTimer() {
	clearTimeout(warningTimeout);
	clearTimeout(logoutTimeout);

	// Set the warning timeout (e.g., 18 minutes)
	warningTimeout = setTimeout(() => {
		showToast("Your session will expire soon due to inactivity.");
	}, 18 * 60 * 1000); // 18 minutes

	// Set the logout timeout (e.g., 20 minutes)
	logoutTimeout = setTimeout(() => {
		showToast("Your session has expired, log in again.");
		window.location.href = "/Identity/Account/Login";
	}, 20 * 60 * 1000); // 20 minutes
}

// Reset the session timer on user activity
document.addEventListener("mousemove", startSessionTimer);
document.addEventListener("keypress", startSessionTimer);

// Start the session timer initially
startSessionTimer();
