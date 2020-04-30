"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const discord_akairo_1 = require("discord-akairo");
const UserDataModel_1 = require("../../../Models/UserDataModel");
const TrelloHelper_1 = __importDefault(require("../../../Helpers/TrelloHelper"));
class TrelloSynchronizateUserCommand extends discord_akairo_1.Command {
    constructor() {
        super('synchronizuj', {
            aliases: ['synchronizuj'],
            category: 'Trello',
            description: {
                content: 'Synchronizuje listę gracza z discordem',
                examples: ['synchronizuj'],
                usage: 'synchronizuj'
            },
            cooldown: 120000,
        });
    }
    // TODO: Pobrawić ten kod jakoś, żeby synchronizate się nie powtarzał bo przeokropnie to wygląda
    async exec(message) {
        const discordUserId = message.author.id;
        const userRepo = this.client.db.getRepository(UserDataModel_1.Users);
        const developer = await userRepo.findOne({ userId: discordUserId });
        if (developer === undefined) {
            await this.synchronizateTrello(message, discordUserId, userRepo, developer);
        }
        else {
            if (developer.trelloCardId !== undefined && developer.trelloCardId !== '') {
                return message.util.reply('❌ Twoje konto jest już zsynchronizowane z kartą na trello!');
            }
            await this.synchronizateTrello(message, discordUserId, userRepo, developer);
        }
        // if(developer == undefined) {
        //     await this.synchronizateTrello(message, discordUserId, userRepo, developer);
        // } else if(developer.trelloCardId !== undefined && developer.trelloCardId !== '') {
        //     return message.util.reply('❌ Twoje konto jest już zsynchronizowane z kartą na trello!');
        // } else {
        //     await this.synchronizateTrello(message, discordUserId, userRepo, developer);
        // }
    }
    async synchronizateTrello(message, discordUserId, userRepo, developer) {
        await TrelloHelper_1.default.createUserList(discordUserId).then(async (res) => {
            await TrelloHelper_1.default.isUserDiscordHaveList(discordUserId).then(async (listId) => {
                if (listId == undefined) {
                    return message.util.reply('Upss... Coś poszło nie tak! Spróbuj ponownie później');
                }
                if (developer === undefined) {
                    await userRepo.insert({
                        userId: discordUserId,
                        trelloCardId: listId
                    });
                }
                else {
                    developer.trelloCardId = listId;
                    await userRepo.save(developer);
                }
                return message.util.reply('✅ Utworzono i zsynchronizowano listę z twoim discordem!');
            });
        });
    }
}
exports.default = TrelloSynchronizateUserCommand;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiVHJlbGxvU3luY2hyb25pemF0ZVVzZXJDb21tYW5kLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vLi4vLi4vLi4vc3JjL0NvbW1hbmRzL0Jhc2UvVHJlbGxvQ29tbWFuZHMvVHJlbGxvU3luY2hyb25pemF0ZVVzZXJDb21tYW5kLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs7O0FBQUEsbURBQXlDO0FBSXpDLGlFQUFzRDtBQUN0RCxpRkFBeUQ7QUFFekQsTUFBcUIsOEJBQStCLFNBQVEsd0JBQU87SUFDL0Q7UUFDSSxLQUFLLENBQUMsY0FBYyxFQUFFO1lBQ2xCLE9BQU8sRUFBRSxDQUFDLGNBQWMsQ0FBQztZQUN6QixRQUFRLEVBQUUsUUFBUTtZQUNsQixXQUFXLEVBQUU7Z0JBQ1QsT0FBTyxFQUFFLHdDQUF3QztnQkFDakQsUUFBUSxFQUFFLENBQUMsY0FBYyxDQUFDO2dCQUMxQixLQUFLLEVBQUUsY0FBYzthQUN4QjtZQUNELFFBQVEsRUFBRSxNQUFNO1NBRW5CLENBQUMsQ0FBQztJQUNQLENBQUM7SUFFRCxnR0FBZ0c7SUFDekYsS0FBSyxDQUFDLElBQUksQ0FBQyxPQUFnQjtRQUM5QixNQUFNLGFBQWEsR0FBVyxPQUFPLENBQUMsTUFBTSxDQUFDLEVBQUUsQ0FBQztRQUNoRCxNQUFNLFFBQVEsR0FBc0IsSUFBSSxDQUFDLE1BQU0sQ0FBQyxFQUFFLENBQUMsYUFBYSxDQUFDLHFCQUFLLENBQUMsQ0FBQztRQUV4RSxNQUFNLFNBQVMsR0FBRyxNQUFNLFFBQVEsQ0FBQyxPQUFPLENBQUMsRUFBRSxNQUFNLEVBQUUsYUFBYSxFQUFFLENBQUMsQ0FBQztRQUVwRSxJQUFHLFNBQVMsS0FBSyxTQUFTLEVBQUU7WUFFeEIsTUFBTSxJQUFJLENBQUMsbUJBQW1CLENBQUMsT0FBTyxFQUFFLGFBQWEsRUFBRSxRQUFRLEVBQUUsU0FBUyxDQUFDLENBQUM7U0FFL0U7YUFBTTtZQUVILElBQUcsU0FBUyxDQUFDLFlBQVksS0FBSyxTQUFTLElBQUksU0FBUyxDQUFDLFlBQVksS0FBSyxFQUFFLEVBQUU7Z0JBQ3RFLE9BQU8sT0FBTyxDQUFDLElBQUksQ0FBQyxLQUFLLENBQUMsNERBQTRELENBQUMsQ0FBQzthQUMzRjtZQUVELE1BQU0sSUFBSSxDQUFDLG1CQUFtQixDQUFDLE9BQU8sRUFBRSxhQUFhLEVBQUUsUUFBUSxFQUFFLFNBQVMsQ0FBQyxDQUFDO1NBQy9FO1FBSUQsK0JBQStCO1FBQy9CLG1GQUFtRjtRQUNuRixxRkFBcUY7UUFDckYsK0ZBQStGO1FBQy9GLFdBQVc7UUFDWCxtRkFBbUY7UUFDbkYsSUFBSTtJQUVSLENBQUM7SUFFTSxLQUFLLENBQUMsbUJBQW1CLENBQUMsT0FBZ0IsRUFBRSxhQUFxQixFQUFFLFFBQTJCLEVBQUUsU0FBZ0I7UUFFbkgsTUFBTSxzQkFBWSxDQUFDLGNBQWMsQ0FBQyxhQUFhLENBQUMsQ0FBQyxJQUFJLENBQUMsS0FBSyxFQUFFLEdBQVEsRUFBRSxFQUFFO1lBQ3JFLE1BQU0sc0JBQVksQ0FBQyxxQkFBcUIsQ0FBQyxhQUFhLENBQUMsQ0FBQyxJQUFJLENBQUMsS0FBSyxFQUFFLE1BQWMsRUFBRSxFQUFFO2dCQUVsRixJQUFJLE1BQU0sSUFBSSxTQUFTLEVBQUU7b0JBQ3JCLE9BQU8sT0FBTyxDQUFDLElBQUksQ0FBQyxLQUFLLENBQUMsc0RBQXNELENBQUMsQ0FBQztpQkFDckY7Z0JBRUQsSUFBRyxTQUFTLEtBQUssU0FBUyxFQUFFO29CQUN4QixNQUFNLFFBQVEsQ0FBQyxNQUFNLENBQUM7d0JBQ2xCLE1BQU0sRUFBRSxhQUFhO3dCQUNyQixZQUFZLEVBQUUsTUFBTTtxQkFDdkIsQ0FBQyxDQUFDO2lCQUNOO3FCQUFNO29CQUNILFNBQVMsQ0FBQyxZQUFZLEdBQUcsTUFBTSxDQUFDO29CQUVoQyxNQUFNLFFBQVEsQ0FBQyxJQUFJLENBQUMsU0FBUyxDQUFDLENBQUM7aUJBQ2xDO2dCQUVELE9BQU8sT0FBTyxDQUFDLElBQUksQ0FBQyxLQUFLLENBQUMseURBQXlELENBQUMsQ0FBQztZQUN6RixDQUFDLENBQUMsQ0FBQztRQUNQLENBQUMsQ0FBQyxDQUFDO0lBQ1AsQ0FBQztDQUNKO0FBdkVELGlEQXVFQyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7IENvbW1hbmQgfSBmcm9tICdkaXNjb3JkLWFrYWlybyc7XHJcbmltcG9ydCB7IE1lc3NhZ2UgfSBmcm9tICdkaXNjb3JkLmpzJztcclxuaW1wb3J0IHsgUmVwb3NpdG9yeSB9IGZyb20gJ3R5cGVvcm0nO1xyXG5cclxuaW1wb3J0IHsgVXNlcnMgfSBmcm9tICcuLi8uLi8uLi9Nb2RlbHMvVXNlckRhdGFNb2RlbCc7XHJcbmltcG9ydCBUcmVsbG9IZWxwZXIgZnJvbSAnLi4vLi4vLi4vSGVscGVycy9UcmVsbG9IZWxwZXInO1xyXG5cclxuZXhwb3J0IGRlZmF1bHQgY2xhc3MgVHJlbGxvU3luY2hyb25pemF0ZVVzZXJDb21tYW5kIGV4dGVuZHMgQ29tbWFuZCB7XHJcbiAgICBjb25zdHJ1Y3RvcigpIHtcclxuICAgICAgICBzdXBlcignc3luY2hyb25penVqJywge1xyXG4gICAgICAgICAgICBhbGlhc2VzOiBbJ3N5bmNocm9uaXp1aiddLFxyXG4gICAgICAgICAgICBjYXRlZ29yeTogJ1RyZWxsbycsXHJcbiAgICAgICAgICAgIGRlc2NyaXB0aW9uOiB7XHJcbiAgICAgICAgICAgICAgICBjb250ZW50OiAnU3luY2hyb25penVqZSBsaXN0xJkgZ3JhY3phIHogZGlzY29yZGVtJyxcclxuICAgICAgICAgICAgICAgIGV4YW1wbGVzOiBbJ3N5bmNocm9uaXp1aiddLFxyXG4gICAgICAgICAgICAgICAgdXNhZ2U6ICdzeW5jaHJvbml6dWonXHJcbiAgICAgICAgICAgIH0sXHJcbiAgICAgICAgICAgIGNvb2xkb3duOiAxMjAwMDAsXHJcbiAgICAgICAgICAgIC8vIHJhdGVsaW1pdDogMixcclxuICAgICAgICB9KTtcclxuICAgIH1cclxuXHJcbiAgICAvLyBUT0RPOiBQb2JyYXdpxIcgdGVuIGtvZCBqYWtvxZssIMW8ZWJ5IHN5bmNocm9uaXphdGUgc2nEmSBuaWUgcG93dGFyemHFgiBibyBwcnplb2tyb3BuaWUgdG8gd3lnbMSFZGFcclxuICAgIHB1YmxpYyBhc3luYyBleGVjKG1lc3NhZ2U6IE1lc3NhZ2UpIHtcclxuICAgICAgICBjb25zdCBkaXNjb3JkVXNlcklkOiBzdHJpbmcgPSBtZXNzYWdlLmF1dGhvci5pZDtcclxuICAgICAgICBjb25zdCB1c2VyUmVwbzogUmVwb3NpdG9yeTxVc2Vycz4gPSB0aGlzLmNsaWVudC5kYi5nZXRSZXBvc2l0b3J5KFVzZXJzKTtcclxuXHJcbiAgICAgICAgY29uc3QgZGV2ZWxvcGVyID0gYXdhaXQgdXNlclJlcG8uZmluZE9uZSh7IHVzZXJJZDogZGlzY29yZFVzZXJJZCB9KTtcclxuICAgICAgICBcclxuICAgICAgICBpZihkZXZlbG9wZXIgPT09IHVuZGVmaW5lZCkge1xyXG5cclxuICAgICAgICAgICAgYXdhaXQgdGhpcy5zeW5jaHJvbml6YXRlVHJlbGxvKG1lc3NhZ2UsIGRpc2NvcmRVc2VySWQsIHVzZXJSZXBvLCBkZXZlbG9wZXIpO1xyXG5cclxuICAgICAgICB9IGVsc2Uge1xyXG5cclxuICAgICAgICAgICAgaWYoZGV2ZWxvcGVyLnRyZWxsb0NhcmRJZCAhPT0gdW5kZWZpbmVkICYmIGRldmVsb3Blci50cmVsbG9DYXJkSWQgIT09ICcnKSB7XHJcbiAgICAgICAgICAgICAgICByZXR1cm4gbWVzc2FnZS51dGlsLnJlcGx5KCfinYwgVHdvamUga29udG8gamVzdCBqdcW8IHpzeW5jaHJvbml6b3dhbmUgeiBrYXJ0xIUgbmEgdHJlbGxvIScpO1xyXG4gICAgICAgICAgICB9XHJcblxyXG4gICAgICAgICAgICBhd2FpdCB0aGlzLnN5bmNocm9uaXphdGVUcmVsbG8obWVzc2FnZSwgZGlzY29yZFVzZXJJZCwgdXNlclJlcG8sIGRldmVsb3Blcik7XHJcbiAgICAgICAgfVxyXG5cclxuICAgICAgICBcclxuXHJcbiAgICAgICAgLy8gaWYoZGV2ZWxvcGVyID09IHVuZGVmaW5lZCkge1xyXG4gICAgICAgIC8vICAgICBhd2FpdCB0aGlzLnN5bmNocm9uaXphdGVUcmVsbG8obWVzc2FnZSwgZGlzY29yZFVzZXJJZCwgdXNlclJlcG8sIGRldmVsb3Blcik7XHJcbiAgICAgICAgLy8gfSBlbHNlIGlmKGRldmVsb3Blci50cmVsbG9DYXJkSWQgIT09IHVuZGVmaW5lZCAmJiBkZXZlbG9wZXIudHJlbGxvQ2FyZElkICE9PSAnJykge1xyXG4gICAgICAgIC8vICAgICByZXR1cm4gbWVzc2FnZS51dGlsLnJlcGx5KCfinYwgVHdvamUga29udG8gamVzdCBqdcW8IHpzeW5jaHJvbml6b3dhbmUgeiBrYXJ0xIUgbmEgdHJlbGxvIScpO1xyXG4gICAgICAgIC8vIH0gZWxzZSB7XHJcbiAgICAgICAgLy8gICAgIGF3YWl0IHRoaXMuc3luY2hyb25pemF0ZVRyZWxsbyhtZXNzYWdlLCBkaXNjb3JkVXNlcklkLCB1c2VyUmVwbywgZGV2ZWxvcGVyKTtcclxuICAgICAgICAvLyB9XHJcblxyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBhc3luYyBzeW5jaHJvbml6YXRlVHJlbGxvKG1lc3NhZ2U6IE1lc3NhZ2UsIGRpc2NvcmRVc2VySWQ6IHN0cmluZywgdXNlclJlcG86IFJlcG9zaXRvcnk8VXNlcnM+LCBkZXZlbG9wZXI6IFVzZXJzKSB7XHJcblxyXG4gICAgICAgIGF3YWl0IFRyZWxsb0hlbHBlci5jcmVhdGVVc2VyTGlzdChkaXNjb3JkVXNlcklkKS50aGVuKGFzeW5jIChyZXM6IGFueSkgPT4ge1xyXG4gICAgICAgICAgICBhd2FpdCBUcmVsbG9IZWxwZXIuaXNVc2VyRGlzY29yZEhhdmVMaXN0KGRpc2NvcmRVc2VySWQpLnRoZW4oYXN5bmMgKGxpc3RJZDogc3RyaW5nKSA9PiB7XHJcblxyXG4gICAgICAgICAgICAgICAgaWYgKGxpc3RJZCA9PSB1bmRlZmluZWQpIHtcclxuICAgICAgICAgICAgICAgICAgICByZXR1cm4gbWVzc2FnZS51dGlsLnJlcGx5KCdVcHNzLi4uIENvxZsgcG9zesWCbyBuaWUgdGFrISBTcHLDs2J1aiBwb25vd25pZSBww7PFum5pZWonKTtcclxuICAgICAgICAgICAgICAgIH1cclxuXHJcbiAgICAgICAgICAgICAgICBpZihkZXZlbG9wZXIgPT09IHVuZGVmaW5lZCkge1xyXG4gICAgICAgICAgICAgICAgICAgIGF3YWl0IHVzZXJSZXBvLmluc2VydCh7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgIHVzZXJJZDogZGlzY29yZFVzZXJJZCxcclxuICAgICAgICAgICAgICAgICAgICAgICAgdHJlbGxvQ2FyZElkOiBsaXN0SWRcclxuICAgICAgICAgICAgICAgICAgICB9KTtcclxuICAgICAgICAgICAgICAgIH0gZWxzZSB7XHJcbiAgICAgICAgICAgICAgICAgICAgZGV2ZWxvcGVyLnRyZWxsb0NhcmRJZCA9IGxpc3RJZDtcclxuXHJcbiAgICAgICAgICAgICAgICAgICAgYXdhaXQgdXNlclJlcG8uc2F2ZShkZXZlbG9wZXIpO1xyXG4gICAgICAgICAgICAgICAgfVxyXG5cclxuICAgICAgICAgICAgICAgIHJldHVybiBtZXNzYWdlLnV0aWwucmVwbHkoJ+KchSBVdHdvcnpvbm8gaSB6c3luY2hyb25pem93YW5vIGxpc3TEmSB6IHR3b2ltIGRpc2NvcmRlbSEnKTtcclxuICAgICAgICAgICAgfSk7XHJcbiAgICAgICAgfSk7XHJcbiAgICB9IFxyXG59Il19