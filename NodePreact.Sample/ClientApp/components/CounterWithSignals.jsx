// @ts-check

import { signal } from "@preact/signals";

// Create a signal that can be subscribed to:
const count = signal(0);

const Counter = () => {
    const increment = () => {
        // A signal is updated by assigning to the `.value` property:
        count.value++;
    }

    const decrement = () => {
        // A signal is updated by assigning to the `.value` property:
        count.value--;
    }

    return (
        <div>
            <p>Count: {count}</p>
            <button onClick={increment}>Increment</button>
            <button onClick={decrement}>Decrement</button>
        </div>
    );
};

export default Counter;
