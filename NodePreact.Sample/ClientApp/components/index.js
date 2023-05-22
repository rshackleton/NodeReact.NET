import Counter from "./Counter";
import CounterWithSignals from "./CounterWithSignals";
import HelloWorld from "./HelloWorld";
import LoremIpsum from "./LoremIpsum";

export const components = {
    Counter,
    CounterWithSignals,
    HelloWorld,
    LoremIpsum,
};

try {
    module.exports = components;
} catch { }
