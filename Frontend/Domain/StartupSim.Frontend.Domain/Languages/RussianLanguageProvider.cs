using StartupSim.Frontend.Domain.Interfaces;

namespace StartupSim.Frontend.Domain.Languages.Russian
{
    public class RussianLanguageProvider : ILanguageProvider
    {
        public string LanguageName
            => "Russian";

        public string[] MaleNames => new[]
        {
            "Юрий",
            "Алексей",
            "Пётр",
            "Квентин",
            "Сэмюэль"
        };

        public string[] MaleSurnames => new[]
        {
            "Сидоров",
            "Джексон",
            "Лентяев",
            "Трудоголиков",
            "Кларк"
        };

        public string[] FemaleNames => new[]
        {
            "Александра",
            "Юлия",
            "Лара",
            "Изабелла",
            "Екатерина"
        };

        public string[] FemaleSurnames => new[]
        {
            "Сидорова",
            "Джексон",
            "Лентяева",
            "Трудоголикова",
            "Кларк"
        };

        public string[] ProjectNames => new[]
        {
            "Социальная сеть",
            "Мессенджер",
            "Интернет-магазин",
            "Видеохостинг",
            "Хостинг картинок",
            "Форум",
            "Новостной портал",
            "Сервис голосований",
            "2d-аркада",
            "Платформер",
            "Гоночная игра",
            "Шутер",
            "Симулятор стартапа",
            "Электронная почта",
            "Графический редактор",
            "Графический редактор Online",
            "Умный калькулятор",
            "Школьный журнал",
            "ИИ",
        };

        public string[] FeaturesNames => new[]
        {
            "Встроенный аудиопроигрыватель",
            "Аудиосообщения",
            "Мини-чат",
        };

        public string[] OpportunityNames => new[]
        {
            "Развод сотрудника",
        };

        public string[] OpportunityDescriptions => new[]
        {
            "Брак вашего сотрудника внезапно распался. Вы любезно предложили пожить в офисе, попутно решив пару рабочих задач.",
        };

        public string[] IncidentNames => new[]
        {
            "Отключение интернета",
            "Сбой электричества",
            "Хакерская атака"
        };

        public string[] IncidentDescriptions => new[]
        {
            "Из-за участившихся перебоев интернета главный провайдер города планирует отключить сеть на профилактику."
            + "В этом случае никто не получит доход на этом спринте. Однако провайдер обещает найти другое решение за "
            + "небольшую помощь...",
            "Объявлена аварийная ситуация на электростанции. В случае сбоя электричества посреди рабочего дня "
            + "все потеряют несохранённые данные. Чтобы избежать это, необходимо собрать деньги на срочный ремонт",
            "Хакеры угрожают удалить по одному репозиторию каждого IT-стартапа и обещают не делать это, если им переведут немного денег."
        };

        public string Singleplayer => "Одиночный режим";

        public string Multiplayer => "Многопользовательский режим";

        public string Options => "Настройки";

        public string Exit => "Выйти";

        public string Back => "Назад";

        public string GameSettings => "Настройки партии";

        public string ConnectionRealTime => "Время подключения (сек.)";

        public string ChoosingBackgroundRealTime => "Время выбора предыстории (сек.)";

        public string SprintRealTime => "Время спринта (сек.)";

        public string DiplomacyRealTime => "Время дипломатии (сек.)";

        public string IncidentRealTime => "Время происшествия (сек.)";

        public string SprintActionsNumbers => "Кол-во действий за спринт";

        public string AuctionRealTime => "Время аукциона (сек.)";

        public string StartUpCapital => "Стартовый капитал";

        public string Start => "Начать!";

        public string SignIn => "Вход";

        public string SignUp => "Регистрация";

        public string Password => "Пароль";

        public string RepeatPassword => "Повторите пароль";

        public string Login => "Логин";

        public string NewGame => "Новая игра";

        public string Connect => "Подключиться";

        public string GamesList => "Список игр";

        public string ConfirmEmail => "Подтвердите Email";

        public string ConfirmationCode => "Код подтверждения";

        public string Confirm => "Подтвердить";

        public string GiveUp => "Сдаться";

        public string Players => "Игроки";

        public string Name => "Имя";

        public string Next => "Далее";

        public string InputName => "Введите имя";

        public string Skip => "Пропустить";

        public string ConnectingPlayers => "Подключение игроков";

        public string TimeLeft => "Времени осталось";

        public string Wait => "Подождите";

        public string ConductInterview => "Провести интервью";

        public string CancelLease => "Отменить аренду";

        public string DismissEveryone => "Уволить всех";

        public string Cancel => "Отмена";

        public string HireTechSupportOfficer => "Нанять сотрудника техподдержки";

        public string[] GameStages => new[]
        {
            "Еще не началась",
            "Подключение",
            "Выбор предыстории",
            "Спринт",
            "Дипломатия",
            "Подведение итогов",
            "Обсуждение инцидента",
            "Разрешение инцидента",
            "Завершена",
        };
        
        public string Default => "По умолчанию";
        
        public string Programmer => "Программист";
        public string Designer => "Дизайнер";
        public string Musician => "Музыкант";
        public string Manager => "Менеджер";
        public string Major => "Мажор";
    }
}