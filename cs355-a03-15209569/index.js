/*
=======================
index.js
=======================
Student ID:15209569
Comment (Required):The Spotify API will be utilized to search for artist. It is based on simple REST principles, the Spotify Web API 
endpoints return JSON metadata about music artists, albums, and tracks, directly from the Spotify Data Catalogue. The album images 
will be displayed and also stored as cache for future use. If the image exist in cache it will be loaded. If not the image will be
displayed and stored in cache.
=======================
*/

//Defines all the required constants/variables needed
const http = require('http');
const https = require('https');
const url = require('url');
const host = 'localhost';
const port = 3000;
const credentials = require('./auth/credentials.json');
const fs = require('fs');
const authentication_cache = './auth/authentication-res.json';
const album_art_path = './album-art/';
const querystring = require('querystring');
let cache_arr = 0;
let album_arr = [];
let image_arr = [];


//Creates a new connection by the returned directory/url request
const new_connection = function(req,res){
    if (req.url === '/') {
        let input_txt = fs.createReadStream('./html/search-form.html');
        res.writeHead(200, {'Content-Type': 'text/html'});
        input_txt.pipe(res);
    }
    else if(req.url.startsWith('/favicon.ico')){
        res.writeHead(404);
        res.end();
	}  
    else  if(req.url.includes('/album-art/')){
        console.log('album-art');
        let artist = url.parse(req.url, true);
        let path = artist.pathname;
        image_stream = fs.createReadStream(`.${decodeURI(path)}`);
        res.writeHead(200,{'Content-Type': 'image/jpeg'});
        image_stream.pipe(res);
        image_stream.on('error', function(err){
            console.log(err);
            res.writeHead(404);
            return res.end();
        });
    }	
    else if (req.url.startsWith('/search')) {
        req.on('data', function (chunk) {request_data += chunk;});
        req.on('end', function () {
            let artist = url.parse(req.url, true).query;
            console.log(artist.q);
            let post_data = querystring.stringify({
                client_id : credentials.client_id,
                client_secret : credentials.client_secret,
                grant_type : 'client_credentials'
                
            });
            let options = {
                method: 'POST',
                headers: {
                    
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'Content-Length': post_data.length
                }
            };
                let cache_status = false;
                if(fs.existsSync(authentication_cache)){
                    let cache_files = fs.readFileSync(authentication_cache, 'utf-8');
                    cache_json = JSON.parse(cache_files);
                    if(new Date(cache_json.expiration) > Date.now()){
                        cache_status = true;
                    }else{
                        console.log('Token is expired');
                    }
                }
                if(cache_status){
                    console.log('already saved to cache');
                    create_search_req(cache_json, res, artist);
                }
                else{
                    const access_token_endpoint = 'https://accounts.spotify.com/api/token';
                    let authentication_sent_time = new Date();
                    let authentication_req = https.request(access_token_endpoint, options, function (authentication_res) {
                    received_authentication(authentication_res, artist, authentication_sent_time, res );
                    });
                    authentication_req.on('error', function(e){
                        console.error(e);
                    });
                    console.log('Requesting Token from API');
                    authentication_req.end(post_data);
                }
           
        });
    }
};

//function to receive authentication and save it for future use
const received_authentication = function(authentication_res, artist, authentication_sent_time, res){
    authentication_res.setEncoding('utf8');
    let body='';
    authentication_res.on('data', function(chunk){body += chunk;});
    authentication_res.on('end', function(){
        let spotify_auth = JSON.parse(body);
        authentication_sent_time.setHours(authentication_sent_time.getHours()+1);
        spotify_auth.expiration = authentication_sent_time;
        console.log(spotify_auth);
        create_cache(spotify_auth);
        console.log('new file added to cache');
        create_search_req(spotify_auth, res, artist);
    })
}

//Creates a search request by use of input from the user
const create_search_req = function(spotify_auth, res, artist){
    console.log(artist);
    let param = {
        access_token : spotify_auth.access_token,
        q : artist.q,
        type : 'album',
        limit:10
    }
    let search_req_url = 'https://api.spotify.com/v1/search?'+querystring.stringify(param);
    console.log(search_req_url);
    let search_req = https.request(search_req_url, function(search_res){
        let results = '';      
        search_res.on('data', function(chunk){results += chunk;});     
        search_res.on('end', function(){
			let search_res_data = JSON.parse(results);         
			for(let i=0; i < param.limit; i++ ){                
                let artist = {
                    name: search_res_data.albums.items[i].name,
                    image: search_res_data.albums.items[i].images[0].url
                }               
                album_arr.push(artist);             
                save_image(album_arr[i], res);
			}
        })
        
    });
    console.log('Generating Album Request');
    search_req.end();
}

//Creates the cache for future use 
const create_cache = function(spotify_auth){
    let cacheJSON = JSON.stringify(spotify_auth);
    fs.writeFile(authentication_cache, cacheJSON, (error)=>{
        if(error){
            console.log('Error occured during attempt to save the token ');
            throw error;
        }
        console.log('The Image has been saved to cache');
    })
}

//downloads the images from the search and stores them locally
const save_image = function (image_url, res){
    let img_path_name = album_art_path + image_url.name+'.jpeg';
    let full_img_path = `<img src='${img_path_name}'>`;
    image_arr.push(full_img_path);
    
    if( fs.existsSync(img_path_name)){
        console.log('The Image has been saved to cache');
        generate_webpage(image_arr, res);
    }
	    let image_req = https.get(image_url.image, function(image_res){
		let new_img = fs.createWriteStream(img_path_name, {'encoding':null});
		image_res.pipe(new_img);
		new_img.on('finish', function() {
			cache_arr += 1;
			if(cache_arr === album_arr.length){
				console.log('Images Displayed');
				generate_webpage(image_arr, res);
			}
		});
    });
	image_req.on('error', function(err){console.log(err);});
};

//generate a webpage with the images from either the spotify request or cache if it exist
const generate_webpage = function(image_arr, res){
    let page = '';
	for (i = 0; i < image_arr.length; i++) { 
         page += image_arr[i];
         res.writeHead(200,{'Content-type': 'text/html'});

    } 
    res.end(page);
}

//create the server and listen on port 3000
const server = http.createServer(new_connection);
server.listen(port, host);
console.log(`Server now listening on Host: ${host}:${port}`);