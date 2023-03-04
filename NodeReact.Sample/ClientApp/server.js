import preact from 'preact';
import render from 'preact-render-to-string';

global.preact = preact;
global.preactRenderToString = render;

require('expose-loader?exposes=Components!./components/index');
