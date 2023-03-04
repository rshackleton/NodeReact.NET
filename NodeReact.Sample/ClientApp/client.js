import preact from 'preact';
import render from 'preact-render-to-string';
import { components } from './components';

window.preact = preact;
window.preactRenderToString = render;

window.Components = components;
