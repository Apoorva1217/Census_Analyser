read a
git branch $a
git checkout $a
git add .
git commit -m "[Apoorva] Add . Ability for the analyser to load the Indian States Code Information from a csv file"
git push origin $a
git checkout master
git merge $a
git push origin master --force
