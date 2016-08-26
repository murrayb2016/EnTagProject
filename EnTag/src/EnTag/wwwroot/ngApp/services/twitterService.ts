namespace EnTag.Services {
    export class TweetTrustService {

        constructor(private $sce: ng.ISCEService) { }
        public getTrustedTweets(tweets) {
            console.log("gettin some trusted tweets");
            /*
            for each tweet, iterate first over media array and then over the urls array
            */

            //Iterate over every tweet
            for (let i = 0; i < tweets.length; i++) {

                let t = tweets[i];                      // The tweet
                let text: string = t.text;				// Tweet text
                let media = t.tweetDTO.entities.media;  // Tweet media
                let urls = t.tweetDTO.entities.urls;    // Tweet urls

                // Iterate backward over the media and urls; backward makes the indices work much easier.
                if (media != null) {
                    // Iterate backward over the media
                    for (let i2 = media.length - 1; i2 >= 0; i2--) {
                        let mediaItem = media[i2];          // Individual media item
                        let idx1 = mediaItem.indices[0];    // Index of where media item starts
                        let idx2 = mediaItem.indices[1];    // Index of where media item ends

                        // If this is true, Twitter has provided a shortened link
                        if (text.charAt(idx1) == "…") {
                            // Checks if the next for characters are "http" and backs up the index
                            // Second condition is to opt for a less catastrophic bug if this doesn't work universally
                            while (text.substr(idx1, 4) != "http" && idx1 > 0)
                                idx1--;
                        }


                        console.log("Media Item: " + text.substring(idx1, idx2 - idx1));
                        console.log("idx1 : " + idx1 + ", " + text.charAt(idx1));
                        console.log("idx2 : " + idx2 + ", " + text.charAt(idx2));
                        console.log("text before: " + text);

                        text = text.substr(0, idx1) + // Text from beginning to the first media item
                            `<a href="` + mediaItem.url + `">` + // adds an <a> tag
                            mediaItem.display_url + "</a>" + // adds the t.co link and ends </a> tag
                            text.substr(idx2);  // The rest

                        console.log("text after: " + text + "\n\n");
                    }
                }
                if (urls != null) {
                    // Iterate backward over the URLs
                    for (let i2 = urls.length - 1; i2 >= 0; i2--) {
                        let urlItem = urls[i2]; // Individual url item
                        let idx1 = urlItem.indices[0];  // Index of where url item starts
                        let idx2 = urlItem.indices[1];  // Index of where url item ends

                        if (text.charAt(idx1) == "…") {
                            while (text.substr(idx1, 4) != "http")
                                idx1--;
                        }

                        console.log("URL Item: " + text.substring(idx1, idx2 - idx1));
                        console.log("idx1 : " + idx1 + ", " + text.charAt(idx1));
                        console.log("idx2 : " + idx2 + ", " + text.charAt(idx2));
                        console.log("text before: " + text);

                        text = text.substr(0, idx1) +   // Text from beginning to the first url item
                            `<a href="` + urlItem.url + `">` +   // adds an <a> tag
                            urlItem.display_url + "</a>" +   // adds the t.co link and ends </a> tag
                            text.substr(idx2);  // The rest

                        console.log("text after: " + text);
                    }
                }



                t.trustedHtml = this.$sce.trustAsHtml(text); // You can call $sce.getTrustedHTML on this
            }
        }

        public log(s: string, b: boolean) {
            if (b)
                console.log(s);
        }
    }
    angular.module('EnTag').service('tweetTrustService', TweetTrustService);
}