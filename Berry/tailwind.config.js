module.exports = {
    purge: {
        enabled: true,
        content: [
            './Pages/**/*.cshtml',
            './Views/**/*.chstml'
        ]
    },
    darkMode: false, // or 'media' or 'class'
    theme: {
        extend: {},

        colours: {
            transparent: 'transparent',
            current: 'currentColor',,
            'light-magenta': '#92374d',
            'light-mint': '#cbf3f0',
            'light-white': '#e9e8ed',
            'light-tangerine': '#f68f5e',
            'light-bittersweet': '#f76d5f',
            'dark-majorelle': '#574AE2',
            'dark-royal': '#222A68',
            'dark-hunter': '#4B644A',
            'dark-ash': '#c7d6d5',
            'dark-quartz': '#9C92A3',
        }
    },
    variants: {
        extend: {},
    },
    plugins: [],
}