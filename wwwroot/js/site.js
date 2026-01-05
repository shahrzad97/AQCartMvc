// ===============================
// THEME HANDLING (FINAL)
// ===============================

document.addEventListener("DOMContentLoaded", () => {
    const toggleBtn = document.getElementById("themeToggle");
    const root = document.documentElement;

    function applyTheme(theme) {
        root.setAttribute("data-theme", theme);
        localStorage.setItem("theme", theme);
        toggleBtn.textContent = theme === "dark" ? "☀️ Light" : "🌙 Dark";
    }

    // Load saved theme OR OS preference
    const savedTheme = localStorage.getItem("theme");
    if (savedTheme) {
        applyTheme(savedTheme);
    } else if (window.matchMedia("(prefers-color-scheme: dark)").matches) {
        applyTheme("dark");
    } else {
        applyTheme("light");
    }

    // Toggle on click
    toggleBtn.addEventListener("click", () => {
        const current = root.getAttribute("data-theme");
        applyTheme(current === "dark" ? "light" : "dark");
    });
});
