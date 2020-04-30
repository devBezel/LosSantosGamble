"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const TrelloModule_1 = require("../Modules/TrelloModule");
const Config_1 = require("../Config");
class TrelloHelper {
    static async isUserDiscordHaveList(userId) {
        let valueToReturn;
        await TrelloModule_1.getTrelloBoardLists(Config_1.trelloBoardId).then((lists) => {
            for (let list of lists) {
                console.log(`${list.name} [${userId === list.name}]`);
                if (userId === list.name) {
                    valueToReturn = list.id;
                    break;
                }
                else {
                    valueToReturn = undefined;
                    break;
                }
            }
        });
        console.log(`valueToReturn ${valueToReturn}`);
        return valueToReturn;
    }
    static async createUserList(userId) {
        let response;
        await TrelloModule_1.createTrelloList(userId, Config_1.trelloBoardId).then((res) => {
            response = res;
        });
        return response;
    }
    static async createCardForUserList(trelloListId, sender, task) {
        let response;
        await TrelloModule_1.createTrelloCardForList(trelloListId, sender, task).then((res) => {
            response = res;
        });
        return response;
    }
}
exports.default = TrelloHelper;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiVHJlbGxvSGVscGVyLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vLi4vc3JjL0hlbHBlcnMvVHJlbGxvSGVscGVyLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7O0FBQUEsMERBQXlHO0FBQ3pHLHNDQUF5QztBQUl6QyxNQUFxQixZQUFZO0lBRXRCLE1BQU0sQ0FBQyxLQUFLLENBQUMscUJBQXFCLENBQUMsTUFBYztRQUNwRCxJQUFJLGFBQWEsQ0FBQztRQUVsQixNQUFNLGtDQUFtQixDQUFDLHNCQUFhLENBQUMsQ0FBQyxJQUFJLENBQUMsQ0FBQyxLQUF3QixFQUFFLEVBQUU7WUFDdkUsS0FBSSxJQUFJLElBQUksSUFBSSxLQUFLLEVBQUU7Z0JBQ25CLE9BQU8sQ0FBQyxHQUFHLENBQUMsR0FBRyxJQUFJLENBQUMsSUFBSSxLQUFLLE1BQU0sS0FBSyxJQUFJLENBQUMsSUFBSSxHQUFHLENBQUMsQ0FBQztnQkFDdEQsSUFBSSxNQUFNLEtBQUssSUFBSSxDQUFDLElBQUksRUFBRTtvQkFDdEIsYUFBYSxHQUFHLElBQUksQ0FBQyxFQUFFLENBQUM7b0JBQ3hCLE1BQU07aUJBQ1Q7cUJBQU07b0JBQ0gsYUFBYSxHQUFHLFNBQVMsQ0FBQztvQkFDMUIsTUFBTTtpQkFDVDthQUNKO1FBQ0wsQ0FBQyxDQUFDLENBQUM7UUFDSCxPQUFPLENBQUMsR0FBRyxDQUFDLGlCQUFpQixhQUFhLEVBQUUsQ0FBQyxDQUFDO1FBQzlDLE9BQU8sYUFBYSxDQUFDO0lBQ3pCLENBQUM7SUFFTSxNQUFNLENBQUMsS0FBSyxDQUFDLGNBQWMsQ0FBQyxNQUFjO1FBQzdDLElBQUksUUFBUSxDQUFDO1FBQ2IsTUFBTSwrQkFBZ0IsQ0FBQyxNQUFNLEVBQUUsc0JBQWEsQ0FBQyxDQUFDLElBQUksQ0FBQyxDQUFDLEdBQVEsRUFBRSxFQUFFO1lBQzVELFFBQVEsR0FBRyxHQUFHLENBQUM7UUFDbkIsQ0FBQyxDQUFDLENBQUM7UUFFSCxPQUFPLFFBQVEsQ0FBQztJQUNwQixDQUFDO0lBRU0sTUFBTSxDQUFDLEtBQUssQ0FBQyxxQkFBcUIsQ0FBQyxZQUFvQixFQUFFLE1BQWMsRUFBRSxJQUFZO1FBQ3hGLElBQUksUUFBUSxDQUFDO1FBQ2IsTUFBTSxzQ0FBdUIsQ0FBQyxZQUFZLEVBQUUsTUFBTSxFQUFFLElBQUksQ0FBQyxDQUFDLElBQUksQ0FBQyxDQUFDLEdBQVEsRUFBRSxFQUFFO1lBQ3hFLFFBQVEsR0FBRyxHQUFHLENBQUM7UUFDbkIsQ0FBQyxDQUFDLENBQUM7UUFFSCxPQUFPLFFBQVEsQ0FBQztJQUNwQixDQUFDO0NBQ0o7QUF0Q0QsK0JBc0NDIiwic291cmNlc0NvbnRlbnQiOlsiaW1wb3J0IHsgZ2V0VHJlbGxvQm9hcmRMaXN0cywgY3JlYXRlVHJlbGxvTGlzdCwgY3JlYXRlVHJlbGxvQ2FyZEZvckxpc3QgfSBmcm9tICcuLi9Nb2R1bGVzL1RyZWxsb01vZHVsZSc7XHJcbmltcG9ydCB7IHRyZWxsb0JvYXJkSWQgfSBmcm9tICcuLi9Db25maWcnXHJcbmltcG9ydCB7IFRyZWxsb0xpc3RNb2RlbCB9IGZyb20gJy4uL01vZGVscy9UcmVsbG9MaXN0TW9kZWwnO1xyXG5cclxuXHJcbmV4cG9ydCBkZWZhdWx0IGNsYXNzIFRyZWxsb0hlbHBlciB7XHJcblxyXG4gICAgcHVibGljIHN0YXRpYyBhc3luYyBpc1VzZXJEaXNjb3JkSGF2ZUxpc3QodXNlcklkOiBzdHJpbmcpOiBQcm9taXNlPHN0cmluZz4ge1xyXG4gICAgICAgIGxldCB2YWx1ZVRvUmV0dXJuO1xyXG5cclxuICAgICAgICBhd2FpdCBnZXRUcmVsbG9Cb2FyZExpc3RzKHRyZWxsb0JvYXJkSWQpLnRoZW4oKGxpc3RzOiBUcmVsbG9MaXN0TW9kZWxbXSkgPT4ge1xyXG4gICAgICAgICAgICBmb3IobGV0IGxpc3Qgb2YgbGlzdHMpIHtcclxuICAgICAgICAgICAgICAgIGNvbnNvbGUubG9nKGAke2xpc3QubmFtZX0gWyR7dXNlcklkID09PSBsaXN0Lm5hbWV9XWApO1xyXG4gICAgICAgICAgICAgICAgaWYgKHVzZXJJZCA9PT0gbGlzdC5uYW1lKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgdmFsdWVUb1JldHVybiA9IGxpc3QuaWQ7XHJcbiAgICAgICAgICAgICAgICAgICAgYnJlYWs7XHJcbiAgICAgICAgICAgICAgICB9IGVsc2Uge1xyXG4gICAgICAgICAgICAgICAgICAgIHZhbHVlVG9SZXR1cm4gPSB1bmRlZmluZWQ7XHJcbiAgICAgICAgICAgICAgICAgICAgYnJlYWs7XHJcbiAgICAgICAgICAgICAgICB9XHJcbiAgICAgICAgICAgIH1cclxuICAgICAgICB9KTtcclxuICAgICAgICBjb25zb2xlLmxvZyhgdmFsdWVUb1JldHVybiAke3ZhbHVlVG9SZXR1cm59YCk7XHJcbiAgICAgICAgcmV0dXJuIHZhbHVlVG9SZXR1cm47XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIHN0YXRpYyBhc3luYyBjcmVhdGVVc2VyTGlzdCh1c2VySWQ6IHN0cmluZykge1xyXG4gICAgICAgIGxldCByZXNwb25zZTtcclxuICAgICAgICBhd2FpdCBjcmVhdGVUcmVsbG9MaXN0KHVzZXJJZCwgdHJlbGxvQm9hcmRJZCkudGhlbigocmVzOiBhbnkpID0+IHtcclxuICAgICAgICAgICAgcmVzcG9uc2UgPSByZXM7XHJcbiAgICAgICAgfSk7XHJcblxyXG4gICAgICAgIHJldHVybiByZXNwb25zZTtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgc3RhdGljIGFzeW5jIGNyZWF0ZUNhcmRGb3JVc2VyTGlzdCh0cmVsbG9MaXN0SWQ6IHN0cmluZywgc2VuZGVyOiBzdHJpbmcsIHRhc2s6IHN0cmluZykge1xyXG4gICAgICAgIGxldCByZXNwb25zZTtcclxuICAgICAgICBhd2FpdCBjcmVhdGVUcmVsbG9DYXJkRm9yTGlzdCh0cmVsbG9MaXN0SWQsIHNlbmRlciwgdGFzaykudGhlbigocmVzOiBhbnkpID0+IHtcclxuICAgICAgICAgICAgcmVzcG9uc2UgPSByZXM7XHJcbiAgICAgICAgfSk7XHJcblxyXG4gICAgICAgIHJldHVybiByZXNwb25zZTtcclxuICAgIH1cclxufSJdfQ==