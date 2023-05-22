const filewatcher = require("filewatcher");

const requireFiles = process.env.NODEPREACT_REQUIREFILES.split(",").map((t) =>
    t.replace(/\\/g, "/")
);
const fileWatcherDebounce = parseInt(process.env.NODEPREACT_FILEWATCHERDEBOUNCE);

const watcher = filewatcher({
    debounce: fileWatcherDebounce, // debounce events in non-polling mode by 10ms
});

requireFiles.map((t) => watcher.add(t));

watcher.on("change", () => {
    process.exit(0);
});

requireFiles.map(__non_webpack_require__);

const renderComponent = (callback, componentId, options, props) => {
    try {
        const component = resolveComponent(global, options.componentName);

        const res = global.preactRenderToString(
            global.preact.h(
                component,
                Object.assign(props, {
                    location: options.location || "",
                    context: {},
                })
            )
        );

        callback(null, res);
    } catch (err) {
        callback(err, null);
    }
};

const resolveComponent = (object, path, defaultValue) => {
    let current = object;
    const pathArray = typeof path === "string" ? path.split(".") : path;

    for (const prop of pathArray) {
        if (current == null) {
            return defaultValue;
        }

        current = current[prop];
    }

    return current == null ? defaultValue : current;
};

export { renderComponent };
