#!/bin/bash
# declare -a arr=("size_5" "size_10" "size_20" "size_50" "size_100")
declare -a arr=("size_5_intermediate" "size_10_intermediate" "size_20_intermediate" "size_50_intermediate" "size_100_intermediate")

# declare -a arr=("size_5")

for i in "${arr[@]}"
do 
    echo "VALIDATING :::: $i"
    rm datasets/seadronesee/labels/val.cache 2> /dev/null
    mv datasets/seadronesee/labels/val_tfm datasets/seadronesee/labels/val

    LIBPATH=lib/yolov5-master
    NAME=tfm_v4_sds_$i # <--- MODEL CHANGE
    # MODELPATH=$LIBPATH/runs/train/$NAME/weights/best.pt
    MODELPATH=$LIBPATH/runs/train/tfm_cross_v4/$NAME/weights/best.pt

    python $LIBPATH/val.py \
        --data datasets/seadronesee/data_tfm.yaml \
        --name ${NAME}_sds \
        --weights $MODELPATH \
        --save-txt \
        --save-json \
        --save-conf \

    echo ".... renaming....."
    mv datasets/seadronesee/labels/val datasets/seadronesee/labels/val_tfm
    echo "ENDED WITH SUCCESS!"

done
