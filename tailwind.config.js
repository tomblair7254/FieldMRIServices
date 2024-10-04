/** @type {import('tailwindcss').Config} */
module.exports = {
    mode: "jit",
    content: [
        "./Pages/**/*.{razor,cshtml}",
        "./Shared/**/*.{razor,cshtml}",
        "./Areas/**/*.{razor,cshtml}",
        "./Views/**/*.{razor,cshtml}",
        // Include any other paths where you use Tailwind classes
    ],
    theme: {
        extend: {
            fontFamily: {
                excon: ['Excon', 'sans-serif'],
                alpino: ['Alpino', 'sans-serif'],
            },
        },
    },
    plugins: [],
};