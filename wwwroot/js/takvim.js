document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        headerToolbar: {
            left: 'prev,next today',
            center: 'title', 
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek' 
        },
        buttonText: {
            today: 'Bugün',         
            month: 'Ay',           
            week: 'Hafta',         
            day: 'Gün',            
            list: 'Ajanda'         
        },
        locale: 'tr', 
        views: {
            timeGridDay: {
                titleFormat: { year: 'numeric', month: 'long', day: 'numeric', weekday: 'long' },
                slotDuration: '00:30',
                slotLabelFormat: { hour: 'numeric', minute: '2-digit', hour12: false }
            },
            listWeek: {
                titleFormat: { year: 'numeric', month: 'long', day: 'numeric' },
                dayHeaderFormat: { weekday: 'long' }
            }
        },
        events: '/Takvim/GetEvents', 
        editable: true, 
        selectable: true, 
        nowIndicator: true,
        height: 'auto',
        themeSystem: 'standard',


        eventDidMount: function (info) {
            info.el.style.whiteSpace = 'normal'; 
            info.el.style.wordWrap = 'break-word'; 
            info.el.style.lineHeight = '1.2'; 
        }
    });

    calendar.render();
});
