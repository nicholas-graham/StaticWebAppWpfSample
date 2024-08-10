# WPF WebView2 Static HTML Sample

Simple app to showcase how WPF, WebView2, and ASP.NET core could be used to self serve statically generated HTML. 

## 🔎 Why?
Mainly an exploration into serving Static HTML as the whole UI via WebView2 and a slim static web server within a WPF app. 

## ⚡️ Quick Start
Clone this repo

Build the WPF app

You should be able to run the `(Production)` version by starting the `StartApp (Production)` launch profile and play around with the different sample commands. 

You can use any static web generator of your choice, in this sample I was using `Astro` as it is a very flexible way to generate static sites. 

If you want to modify the `Astro` site included under `StantecWebAppWpf.Web`, you'll need to [**get started**](https://docs.astro.build/en/getting-started/) with `Astro`. 

Simply open the `StantecWebAppWpf.Web` folder in VsCode or your editor of choice and run `npm i` followed by `npm run dev`, then start the Wpf application. 

It is pre-configured to point to the `dev` server for live development!

## ⚠️ License
This project is covered under the MIT license. 