#!/bin/bash

rm datasets/seadronesee/labels/val.cache 2> /dev/null
mv datasets/seadronesee/labels/val_tfm datasets/seadronesee/labels/val

LIBPATH=lib/yolov5-master
NAME=tfm_v4_unfrozen_sds_size_50 # <--- MODEL CHANGE
MODELPATH=$LIBPATH/runs/train/$NAME/weights/best.pt

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

# tfm dataset validation 
# python $LIBPATH/val.py --data datasets/tfm/data.yaml --name tfm_17_06_tfm_resume --weights $LIBPATH/runs/train/17_06_tfm_resume/weights/best.pt # --conf-thres 0.5  # checkpoint to trained model
# python $LIBPATH/val.py --data datasets/tfm/data_coco.yaml --name tfm_17_06_coco_pretrained --weights $LIBPATH/yolov5s.pt # --conf-thres 0.5  # checkpoint to trained model

