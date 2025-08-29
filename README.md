🤖 AutoClicker CloudFlare v1.0 - (File-Based Status)
<p align="center">
<img src="https://www.google.com/search?q=https://img.shields.io/badge/Language-C%2523-blueviolet%3Fstyle%3Dfor-the-badge" alt="Language C#">
<img src="https://www.google.com/search?q=https://img.shields.io/badge/.NET-Framework-blue%3Fstyle%3Dfor-the-badge" alt=".NET Framework">
<img src="https://www.google.com/search?q=https://img.shields.io/badge/Platform-Windows-0078D6%3Fstyle%3Dfor-the-badge" alt="Platform Windows">
<img src="https://www.google.com/search?q=https://img.shields.io/badge/License-MIT-green%3Fstyle%3Dfor-the-badge" alt="License MIT">
</p>

هذا البرنامج هو أداة بسيطة تعمل تلقائيًا للبحث عن صورة معينة (checkbox) على الشاشة، والنقر عليها، ثم التحقق من ظهور صورة أخرى (True image) لتأكيد نجاح العملية.

⚙️ آلية العمل
البحث المستمر: يقوم البرنامج بالبحث بشكل مستمر عن إحدى صور checkbox.png أو checkbox2.png على الشاشة.

النقر التلقائي: عند العثور على صورة الـ checkbox، يقوم البرنامج بمحاكاة نقرة الفأرة في منتصف الصورة.

التحقق من النجاح: بعد النقر، ينتظر البرنامج لمدة 5 ثوانٍ ثم يبحث عن إحدى صور TrueL.png أو TrueD.png.

تحديث الحالة:

إذا تم العثور على صورة "True"، يقوم البرنامج بإنشاء أو تحديث ملف نصي اسمه autoclicker_status.txt ويكتب بداخله كلمة True.

إذا لم يتم العثور على صورة "True" (أو لم يتم العثور على checkbox من البداية)، سيتم كتابة كلمة False في الملف.

التكرار: تستمر هذه العملية في حلقة لا نهائية، مما يسمح للبرنامج بالعمل باستمرار في الخلفية.

🚀 كيفية الاستخدام
المتطلبات الأساسية
نظام تشغيل Windows.

حزمة .NET Framework.

مكتبة OpenCvSharp4.

خطوات التشغيل
ملفات الصور: تأكد من وجود ملفات الصور (checkbox.png, checkbox2.png, TrueL.png, TrueD.png) في نفس المجلد الذي يوجد به الملف التنفيذي للبرنامج.

التشغيل: قم بتشغيل الملف التنفيذي للبرنامج AutoClicker.exe.

التواصل مع السكريبتات الأخرى: يمكن لأي سكريبت أو برنامج آخر معرفة حالة الـ AutoClicker عن طريق قراءة محتوى ملف autoclicker_status.txt بشكل دوري.

📝 ملاحظات
هذه النسخة تعتمد على عمليات القراءة والكتابة على القرص الصلب (I/O)، والتي قد تكون بطيئة نسبيًا.

يجب أن يكون للبرنامج صلاحية للوصول للكتابة في المجلد الذي يعمل فيه لإنشاء ملف الحالة.
