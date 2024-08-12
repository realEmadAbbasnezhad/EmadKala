echo "updating bootstrap..."
rm -rf ../wwwroot/lib/bootstrap/*
sass bootstrap.scss --style compressed > ../wwwroot/lib/bootstrap/bootstrap.css
cp lib/bootstrap/js/bootstrap.bundle.min.js ../wwwroot/lib/bootstrap/bootstrap.js

echo "updating jquery..."
rm -rf ../wwwroot/lib/jquery/*
cp lib/jquery/jquery.min.js ../wwwroot/lib/jquery/jquery.js

echo "updating bootstrap-icons..."
rm -rf ../wwwroot/lib/bootstrap-icons/*
cp -r lib/bootstrap-icons/font/fonts ../wwwroot/lib/bootstrap-icons/
cp lib/bootstrap-icons/font/bootstrap-icons.min.css ../wwwroot/lib/bootstrap-icons/bootstrap-icons.css

echo "updating frontend finished!"
