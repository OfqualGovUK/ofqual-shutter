# Ofqual Shutter Pages

## Introduction

This is a static site that is used whenever Ofqual needs to temporarily disable a website in the event of a problem

This site uses Vite to build a vanilla HTML/CSS/JS application, deploying out to an Azure Static Web App site

## Rules for use

- Adjustments to **content** should only be made in index.html for the Title and within the bounds of the content comment
- Adjustments to other aspects of the system (e.g. packages) should only be undertaken by a dedicated developer
- All changes must be submitted through the pull request system and preferably reviewed by another person
- All changes must pass a Snyk code scan without raising issues

## Run instructions

- To run and maintain this repository locally, you must have the following installed
    - Node.js v20 or later
    - Git Bash
- To run this repository locally...
    - Install the packages required using `npm install`
    - Then run `npm run dev`; this will start a web server. This will output a server URL that is running the site (usually [http://localhost:5173/](http://localhost:5173/))
- To make changes...
    - You must checkout a branch of this repository; pushes to main will be rejected. A branch must be pushed out to Github and the Pull Request functionality used to make a change
    - For non-devs, changes should only be made to index.html. Changes in this file will be reflected in the running local server every time you save.