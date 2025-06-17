Bu proje internet sayesinde çalısan .net core framework ile yazılmıs kişisel bir borsa sitesidir. Kaydolan kişi portfoyüne amerikan hissesi ekleyebilir, aratarak anlık hisse fiyatlarına ve hisse fiyatlarının yakın gelecekteki tahmini değerine ulaabilir. Bu proje kapsamında:
Katmanlı mimari (controller, service, repository) kullanarak okunabilir ve sürdürülebilir bir backend yapısı kurdum.

JWT token ile kullanıcı girişi ve kimlik doğrulama sistemini kurdum, kullanıcı bilgilerinin token içinde taşınmasını sağlayarak oturum yönetimini güvenli hale getirdim.

Token üzerinden userId bilgisini okuyarak kullanıcıya özel portföy verilerini veritabanından filtreleyip gösterdim.

İlişkisel veritabanı yapısını oluşturarak users, portfolio gibi tablolar arasında foreign key ilişkileri kurdum.

SQL sorgularında join, filtreleme ve sıralama gibi işlemler üzerinde çalışarak veri bütünlüğü ve performans açısından sağlam sorgular yazmayı öğrendim.

Kullanıcıların şifrelerini veritabanında hashleyerek sakladım, böylece temel kullanıcı güvenliği prensiplerini uyguladım.

Gerçek zamanlı hisse verilerini dış API’lerden JSON formatında çekerek backend tarafında işledim.

API'den gelen JSON verileriyle çalışarak şemasız veri yapısı mantığını öğrendim, bu sayede NoSQL veri modellemesine alıştım.

Python ile Flask tabanlı ayrı bir tahmin API’si geliştirdim. Bu API, istatistiksel analiz ve zaman serisi modelleme (örneğin ARIMA) kullanarak hisse senedi fiyat tahminleri üretmektedir.

Geliştirdiğim bu tahmin API’sini borsa sitesine entegre ederek gerçek veri üzerinden öngörüler sunmayı başardım. Hem backend tarafında hem tahmin yapan api tarafında 2 farklı server yazarak, Ayrıca external apilerden serviceler ile .net serverımdan request ile veri cekerek clienta zengin bir hizmet sağlamayı öğrendim.

Frontend tarafında TradingView'in sağladığı veri widget’ını kullanarak hisse grafiklerini websocket yazmadan, kolayca kullanıcıya sundum.

Kullanıcının hisse alım işlemleri, portföy güncellemeleri gibi etkileşimli işlemleri veritabanına yazacak şekilde senaryolar oluşturdum.

Geliştirme sürecinde karşılaştığım teknik problemleri çözmek için araştırmalar yaptım ve bu sayede öğrenme becerimi geliştirdim.

Gerçek bir projeyi baştan sona geliştirerek backend mimarisi, veritabanı tasarımı, API entegrasyonu, veri güvenliği ve frontend bağlantıları gibi birçok konuda bütünsel bir yazılım geliştirme deneyimi yaşadım.


