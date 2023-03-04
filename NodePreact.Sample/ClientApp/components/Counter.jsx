// @ts-check

import { useState } from 'preact/hooks';

/**
 * @param {object} props
 * @param {string} props.name
 */
const Counter = ({ initialValue }) => {
    const [count, setCount] = useState(initialValue);
    const increment = () => setCount(count + 1);
    // You can also pass a callback to the setter
    const decrement = () => setCount((currentCount) => currentCount - 1);

    return (
        <div>
            <p>Count: {count}</p>
            <button onClick={increment}>Increment</button>
            <button onClick={decrement}>Decrement</button>
        </div>
    )
};

export default Counter;
