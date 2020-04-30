"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const discord_akairo_1 = require("discord-akairo");
const UserDataModel_1 = require("../../../Models/UserDataModel");
const TrelloHelper_1 = __importDefault(require("../../../Helpers/TrelloHelper"));
class TrelloCreateTaskCommand extends discord_akairo_1.Command {
    constructor() {
        super('blad', {
            aliases: ['blad', 'bug', 'zadanie', 'task'],
            category: 'Trello',
            description: {
                content: 'Zglasza bląd do konkretnego użytkownika na trello',
                examples: ['blad @Algorytm jakis przykladowy blad'],
                usage: 'blad'
            },
            // cooldown: 120000,
            args: [
                {
                    id: 'member',
                    type: 'member',
                    prompt: {
                        start: (msg) => `❔ ${msg.author}, uwzględnij osobę, której chcesz dodać zadanie do listy!`,
                        retry: (msg) => `❔ ${msg.author} proszę... Uwzględnij prawidłową osobę, której chcesz dodać zadanie do listy!`
                    },
                },
                {
                    id: 'task',
                    type: 'string',
                    match: 'text',
                    default: 'Nie wprowadzono treści blędu'
                }
            ]
        });
    }
    async exec(message, { member, task }) {
        const userRepo = this.client.db.getRepository(UserDataModel_1.Users);
        const developer = await userRepo.findOne({ userId: member.id });
        if (developer == undefined) {
            if (developer.trelloCardId == undefined) {
                return message.util.reply('Ta osoba nie ma zsynchronizowanej listy, przekaż zadanie innej');
            }
        }
        await TrelloHelper_1.default.createCardForUserList(developer.trelloCardId, `${message.author.tag} (${message.author.id})`, task).then((res) => {
            message.util.reply(`✅ Utworzyłeś zadanie dla ${member.displayName}!`);
            // TODO: Na podstawie res.id zrobić nowy task i wlączyć w to użytkownika sendera i resolvera który rozwiązał zadanie (sender otrzymuje punkt za wykonane zadanie)
            console.log(`Utworzono card ${res.id}`);
        });
    }
}
exports.default = TrelloCreateTaskCommand;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiVHJlbGxvQ3JlYXRlVGFza0NvbW1hbmQuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyIuLi8uLi8uLi8uLi9zcmMvQ29tbWFuZHMvQmFzZS9UcmVsbG9Db21tYW5kcy9UcmVsbG9DcmVhdGVUYXNrQ29tbWFuZC50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7OztBQUFBLG1EQUF5QztBQUl6QyxpRUFBc0Q7QUFDdEQsaUZBQXlEO0FBRXpELE1BQXFCLHVCQUF3QixTQUFRLHdCQUFPO0lBQ3hEO1FBQ0ksS0FBSyxDQUFDLE1BQU0sRUFBRTtZQUNWLE9BQU8sRUFBRSxDQUFDLE1BQU0sRUFBRSxLQUFLLEVBQUUsU0FBUyxFQUFFLE1BQU0sQ0FBQztZQUMzQyxRQUFRLEVBQUUsUUFBUTtZQUNsQixXQUFXLEVBQUU7Z0JBQ1QsT0FBTyxFQUFFLG1EQUFtRDtnQkFDNUQsUUFBUSxFQUFFLENBQUMsdUNBQXVDLENBQUM7Z0JBQ25ELEtBQUssRUFBRSxNQUFNO2FBQ2hCO1lBQ0Qsb0JBQW9CO1lBQ3BCLElBQUksRUFBRTtnQkFDRjtvQkFDSSxFQUFFLEVBQUUsUUFBUTtvQkFDWixJQUFJLEVBQUUsUUFBUTtvQkFDZCxNQUFNLEVBQUU7d0JBQ0osS0FBSyxFQUFFLENBQUMsR0FBWSxFQUFFLEVBQUUsQ0FBQyxLQUFLLEdBQUcsQ0FBQyxNQUFNLDJEQUEyRDt3QkFDbkcsS0FBSyxFQUFFLENBQUMsR0FBWSxFQUFFLEVBQUUsQ0FBQyxLQUFLLEdBQUcsQ0FBQyxNQUFNLCtFQUErRTtxQkFDMUg7aUJBQ0o7Z0JBQ0Q7b0JBQ0ksRUFBRSxFQUFFLE1BQU07b0JBQ1YsSUFBSSxFQUFFLFFBQVE7b0JBQ2QsS0FBSyxFQUFFLE1BQU07b0JBQ2IsT0FBTyxFQUFFLDhCQUE4QjtpQkFDMUM7YUFDSjtTQUNKLENBQUMsQ0FBQztJQUNQLENBQUM7SUFFTSxLQUFLLENBQUMsSUFBSSxDQUFDLE9BQWdCLEVBQUUsRUFBRSxNQUFNLEVBQUUsSUFBSSxFQUF5QztRQUN2RixNQUFNLFFBQVEsR0FBc0IsSUFBSSxDQUFDLE1BQU0sQ0FBQyxFQUFFLENBQUMsYUFBYSxDQUFDLHFCQUFLLENBQUMsQ0FBQztRQUV4RSxNQUFNLFNBQVMsR0FBRyxNQUFNLFFBQVEsQ0FBQyxPQUFPLENBQUMsRUFBRSxNQUFNLEVBQUUsTUFBTSxDQUFDLEVBQUUsRUFBRSxDQUFDLENBQUM7UUFFaEUsSUFBRyxTQUFTLElBQUksU0FBUyxFQUFFO1lBQ3hCLElBQUcsU0FBUyxDQUFDLFlBQVksSUFBSSxTQUFTLEVBQUU7Z0JBQ25DLE9BQU8sT0FBTyxDQUFDLElBQUksQ0FBQyxLQUFLLENBQUMsZ0VBQWdFLENBQUMsQ0FBQzthQUNoRztTQUNIO1FBRUQsTUFBTSxzQkFBWSxDQUFDLHFCQUFxQixDQUFDLFNBQVMsQ0FBQyxZQUFZLEVBQUUsR0FBRyxPQUFPLENBQUMsTUFBTSxDQUFDLEdBQUcsS0FBSyxPQUFPLENBQUMsTUFBTSxDQUFDLEVBQUUsR0FBRyxFQUFFLElBQUksQ0FBQyxDQUFDLElBQUksQ0FBQyxDQUFDLEdBQVEsRUFBRSxFQUFFO1lBRXJJLE9BQU8sQ0FBQyxJQUFJLENBQUMsS0FBSyxDQUFDLDRCQUE0QixNQUFNLENBQUMsV0FBVyxHQUFHLENBQUMsQ0FBQztZQUV0RSxpS0FBaUs7WUFDakssT0FBTyxDQUFDLEdBQUcsQ0FBQyxrQkFBa0IsR0FBRyxDQUFDLEVBQUUsRUFBRSxDQUFDLENBQUM7UUFDNUMsQ0FBQyxDQUFDLENBQUM7SUFDUCxDQUFDO0NBRUo7QUFsREQsMENBa0RDIiwic291cmNlc0NvbnRlbnQiOlsiaW1wb3J0IHsgQ29tbWFuZCB9IGZyb20gXCJkaXNjb3JkLWFrYWlyb1wiO1xyXG5pbXBvcnQgeyBNZXNzYWdlIH0gZnJvbSBcImRpc2NvcmQuanNcIjtcclxuaW1wb3J0IHsgR3VpbGRNZW1iZXIgfSBmcm9tIFwiZGlzY29yZC5qc1wiO1xyXG5pbXBvcnQgeyBSZXBvc2l0b3J5IH0gZnJvbSBcInR5cGVvcm1cIjtcclxuaW1wb3J0IHsgVXNlcnMgfSBmcm9tIFwiLi4vLi4vLi4vTW9kZWxzL1VzZXJEYXRhTW9kZWxcIjtcclxuaW1wb3J0IFRyZWxsb0hlbHBlciBmcm9tIFwiLi4vLi4vLi4vSGVscGVycy9UcmVsbG9IZWxwZXJcIjtcclxuXHJcbmV4cG9ydCBkZWZhdWx0IGNsYXNzIFRyZWxsb0NyZWF0ZVRhc2tDb21tYW5kIGV4dGVuZHMgQ29tbWFuZCB7XHJcbiAgICBjb25zdHJ1Y3RvcigpIHtcclxuICAgICAgICBzdXBlcignYmxhZCcsIHtcclxuICAgICAgICAgICAgYWxpYXNlczogWydibGFkJywgJ2J1ZycsICd6YWRhbmllJywgJ3Rhc2snXSxcclxuICAgICAgICAgICAgY2F0ZWdvcnk6ICdUcmVsbG8nLCBcclxuICAgICAgICAgICAgZGVzY3JpcHRpb246IHtcclxuICAgICAgICAgICAgICAgIGNvbnRlbnQ6ICdaZ2xhc3phIGJsxIVkIGRvIGtvbmtyZXRuZWdvIHXFvHl0a293bmlrYSBuYSB0cmVsbG8nLFxyXG4gICAgICAgICAgICAgICAgZXhhbXBsZXM6IFsnYmxhZCBAQWxnb3J5dG0gamFraXMgcHJ6eWtsYWRvd3kgYmxhZCddLFxyXG4gICAgICAgICAgICAgICAgdXNhZ2U6ICdibGFkJ1xyXG4gICAgICAgICAgICB9LFxyXG4gICAgICAgICAgICAvLyBjb29sZG93bjogMTIwMDAwLFxyXG4gICAgICAgICAgICBhcmdzOiBbXHJcbiAgICAgICAgICAgICAgICB7XHJcbiAgICAgICAgICAgICAgICAgICAgaWQ6ICdtZW1iZXInLFxyXG4gICAgICAgICAgICAgICAgICAgIHR5cGU6ICdtZW1iZXInLFxyXG4gICAgICAgICAgICAgICAgICAgIHByb21wdDoge1xyXG4gICAgICAgICAgICAgICAgICAgICAgICBzdGFydDogKG1zZzogTWVzc2FnZSkgPT4gYOKdlCAke21zZy5hdXRob3J9LCB1d3pnbMSZZG5paiBvc29ixJksIGt0w7NyZWogY2hjZXN6IGRvZGHEhyB6YWRhbmllIGRvIGxpc3R5IWAsXHJcbiAgICAgICAgICAgICAgICAgICAgICAgIHJldHJ5OiAobXNnOiBNZXNzYWdlKSA9PiBg4p2UICR7bXNnLmF1dGhvcn0gcHJvc3rEmS4uLiBVd3pnbMSZZG5paiBwcmF3aWTFgm93xIUgb3NvYsSZLCBrdMOzcmVqIGNoY2VzeiBkb2RhxIcgemFkYW5pZSBkbyBsaXN0eSFgXHJcbiAgICAgICAgICAgICAgICAgICAgfSxcclxuICAgICAgICAgICAgICAgIH0sXHJcbiAgICAgICAgICAgICAgICB7XHJcbiAgICAgICAgICAgICAgICAgICAgaWQ6ICd0YXNrJyxcclxuICAgICAgICAgICAgICAgICAgICB0eXBlOiAnc3RyaW5nJyxcclxuICAgICAgICAgICAgICAgICAgICBtYXRjaDogJ3RleHQnLFxyXG4gICAgICAgICAgICAgICAgICAgIGRlZmF1bHQ6ICdOaWUgd3Byb3dhZHpvbm8gdHJlxZtjaSBibMSZZHUnXHJcbiAgICAgICAgICAgICAgICB9XHJcbiAgICAgICAgICAgIF1cclxuICAgICAgICB9KTtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgYXN5bmMgZXhlYyhtZXNzYWdlOiBNZXNzYWdlLCB7IG1lbWJlciwgdGFzayB9OiB7IG1lbWJlcjogR3VpbGRNZW1iZXIsIHRhc2s6IHN0cmluZyB9KSB7XHJcbiAgICAgICAgY29uc3QgdXNlclJlcG86IFJlcG9zaXRvcnk8VXNlcnM+ID0gdGhpcy5jbGllbnQuZGIuZ2V0UmVwb3NpdG9yeShVc2Vycyk7XHJcblxyXG4gICAgICAgIGNvbnN0IGRldmVsb3BlciA9IGF3YWl0IHVzZXJSZXBvLmZpbmRPbmUoeyB1c2VySWQ6IG1lbWJlci5pZCB9KTtcclxuICAgICAgICBcclxuICAgICAgICBpZihkZXZlbG9wZXIgPT0gdW5kZWZpbmVkKSB7XHJcbiAgICAgICAgICAgaWYoZGV2ZWxvcGVyLnRyZWxsb0NhcmRJZCA9PSB1bmRlZmluZWQpIHtcclxuICAgICAgICAgICAgICAgIHJldHVybiBtZXNzYWdlLnV0aWwucmVwbHkoJ1RhIG9zb2JhIG5pZSBtYSB6c3luY2hyb25pem93YW5laiBsaXN0eSwgcHJ6ZWthxbwgemFkYW5pZSBpbm5laicpO1xyXG4gICAgICAgICAgIH1cclxuICAgICAgICB9XHJcblxyXG4gICAgICAgIGF3YWl0IFRyZWxsb0hlbHBlci5jcmVhdGVDYXJkRm9yVXNlckxpc3QoZGV2ZWxvcGVyLnRyZWxsb0NhcmRJZCwgYCR7bWVzc2FnZS5hdXRob3IudGFnfSAoJHttZXNzYWdlLmF1dGhvci5pZH0pYCwgdGFzaykudGhlbigocmVzOiBhbnkpID0+IHtcclxuXHJcbiAgICAgICAgICAgIG1lc3NhZ2UudXRpbC5yZXBseShg4pyFIFV0d29yennFgmXFmyB6YWRhbmllIGRsYSAke21lbWJlci5kaXNwbGF5TmFtZX0hYCk7XHJcblxyXG4gICAgICAgICAgICAvLyBUT0RPOiBOYSBwb2RzdGF3aWUgcmVzLmlkIHpyb2JpxIcgbm93eSB0YXNrIGkgd2zEhWN6ecSHIHcgdG8gdcW8eXRrb3duaWthIHNlbmRlcmEgaSByZXNvbHZlcmEga3TDs3J5IHJvendpxIV6YcWCIHphZGFuaWUgKHNlbmRlciBvdHJ6eW11amUgcHVua3QgemEgd3lrb25hbmUgemFkYW5pZSlcclxuICAgICAgICAgICAgY29uc29sZS5sb2coYFV0d29yem9ubyBjYXJkICR7cmVzLmlkfWApO1xyXG4gICAgICAgIH0pO1xyXG4gICAgfVxyXG4gICAgXHJcbn0iXX0=