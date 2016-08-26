namespace EnTag.Services {
    export class TwitchServices {
        constructor(private $http: ng.IHttpService) {

        }

        public getName() {
            var token = "wat";
            this.$http.get('https://api.twitch.tv/kraken?oauth_token=' + token).then((response) => {
                var data = response.data;
                return data[3].user_name;
            });
        }

        public getFollows() {
            var name = this.getName;
            this.$http.get(`https://api.twitch.tv/kraken/users/${name}/follows/channels`)
                .then((response) => {
                    return response.data;
                });
        }

        public getTest() {
            this.$http.get('/api/twitch/username').then((response) => {
                var username = response.data;
                this.$http.get('/api/twitch/follows/' + username).then((response) => {
                    return response.data;
                });
            });
        }

        public getLive() {
            return this.$http.get('/api/twitch/follows/live').then((response) => {
                console.log(response.data);
                return response.data;
            });
        }

        public search(search: string) {
            return this.$http.get('https://api.twitch.tv/kraken/search/streams?q=' + search).then((response) => {
                return response.data;
            });
        }
    }
    angular.module('EnTag').service('twitchService', TwitchServices);
}