import "preact/debug";

import { h, hydrate, render } from 'preact';
import { components } from './components';

window.preact = { h, hydrate, render };

window.Components = components;
